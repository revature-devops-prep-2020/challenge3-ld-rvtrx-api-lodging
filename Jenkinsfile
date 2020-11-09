def getRepoURL() {
  sh "git config --get remote.origin.url > .git/remote-url"
  return readFile(".git/remote-url").trim()
}

def getCommitSha() {
  sh "git rev-parse HEAD > .git/current-commit"
  return readFile(".git/current-commit").trim()
}

def updateGithubCommitStatus(build) {
  // workaround https://issues.jenkins-ci.org/browse/JENKINS-38674
  repoUrl = getRepoURL()
  commitSha = getCommitSha()

  step([
    $class: 'GitHubCommitStatusSetter',
    reposSource: [$class: "ManuallyEnteredRepositorySource", url: repoUrl],
    commitShaSource: [$class: "ManuallyEnteredShaSource", sha: commitSha],
    errorHandlers: [[$class: 'ShallowAnyErrorHandler']],
    statusResultSource: [
      $class: 'ConditionalStatusResultSource',
      results: [
        [$class: 'BetterThanOrEqualBuildResult', result: 'SUCCESS', state: 'SUCCESS', message: build.description],
        [$class: 'BetterThanOrEqualBuildResult', result: 'FAILURE', state: 'FAILURE', message: build.description],
        [$class: 'AnyBuildResult', state: 'FAILURE', message: 'Loophole']
      ]
    ]
  ])
}

def repoName = "reblank/challenge3_lodging"
def versionTag = "1.0.0"
def kubeMaster = "https://my-cluster-aks-group-f6d8e5-0452c04c.hcp.eastus.azmk8s.io:443"


pipeline {
    agent none
    stages {
        stage('build & verify')
        {
            agent {
                docker { 
                    image 'nosinovacao/dotnet-sonar'
                    args '-v $HOME/.nuget/:/root/.nuget -v$HOME/.sonar/cache/:/opt/sonar-scanner/.sonar/cache --net=host' }
            }
            stages{
                stage('Restore') {
                    steps {
                        git url: 'https://github.com/revature-devops-prep-2020/challenge3-ld-rvtrx-api-lodging', branch: "main"
                        dir('aspnet/')
                        {
                            sh 'dotnet restore --packages /root/.nuget'
                        }
                    }
                    post
                    {
                        success{
                            slackSend(color: '#00FF00', message: "${env.JOB_NAME}'s restored dependencies")
                        }
                        failure{
                            slackSend(color: '#FF0000', message: "${env.JOB_NAME}'s failed to restore dependencies.")
                        }
                    }
                }
                stage('Build & Analyze') {
                    steps {
                        withSonarQubeEnv('SonarCloud'){
                            dir('aspnet/')
                            {
                                sh '''dotnet /sonar-scanner/SonarScanner.MSBuild.dll begin \
                                /k:"revature-devops-prep-2020_challenge3-ld-rvtrx-api-lodging" \
                                /o:"revature-devops-prep-2020" /s:"$(pwd)/sonar.analysis.xml"'''
                                sh 'dotnet build '
                            }
                        }
                    }
                    post
                    {
                        success{
                            slackSend(color: '#00FF00', message: "${env.JOB_NAME}'s Tests succeeded")
                        }
                        failure{
                            slackSend(color: '#FF0000', message: "${env.JOB_NAME}'s Tests failed.")
                        }
                    }
                }
                stage('Test') {
                    steps {
                        dir('aspnet/')
                        {
                            withSonarQubeEnv('SonarCloud'){
                            sh '''dotnet test  \
                                -p:CollectCoverage=true \
                                -p:CoverletOutput=../code_coverage/ \
                                -p:CoverletOutputFormat=opencover \
                                --logger trx \
                                --results-directory ./test_coverage/'''
                            sh 'dotnet /sonar-scanner/SonarScanner.MSBuild.dll end'
                            }   
                        }
                    }
                    post
                    {
                        success{
                            slackSend(color: '#00FF00', message: "${env.JOB_NAME}'s Tests succeeded")
                        }
                        failure{
                            slackSend(color: '#FF0000', message: "${env.JOB_NAME}'s Tests failed.")
                        }
                    }
                }
                
                stage('publish') {
                    steps {
                        dir('aspnet/')
                        {
                            sh 'dotnet publish -o ../app --no-restore --no-build'
                        }
                    }
                    post
                    {
                        success{
                            slackSend(color: '#00FF00', message: "${env.JOB_NAME}'s .NET project built successfully.")
                        }
                        failure{
                            slackSend(color: '#FF0000', message: "${env.JOB_NAME}'s .NET project failed to build.")
                        }
                    }
                }
                stage('quality gate'){
                    steps{
                        withSonarQubeEnv('SonarCloud')
                        {
                            timeout(time: 10, unit: 'MINUTES') {
                                // Parameter indicates whether to set pipeline to UNSTABLE if Quality Gate fails
                                // true = set pipeline to UNSTABLE, false = don't
                                waitForQualityGate abortPipeline: true
                            }
                        }
                    }
                    post
                    {
                        success{
                            slackSend(color: '#00FF00', message: "${env.JOB_NAME} passed the quality gate.")
                        }
                        failure{
                            slackSend(color: '#FF0000', message: "${env.JOB_NAME} failed the quality gate.")
                        }
                    }
                }
            }   
        }
        stage("Dockerize")
        {
            steps{
                dockerize(repoName, versionTag, this, false)
            }
        }
        stage("Deployment")
        {
            steps{
                kubeDeploy(kubeMaster, this, 'kube-sa', '.kubernetes/')
            }
        }
    }
    post {
        success {
            node('Master')
            {
                setBuildStatus("Build ${env.BUILD_NUMBER} succeeded", "SUCCESS")
            }  
        }
        failure {
            node('Master')
            {
                setBuildStatus("Build ${env.BUILD_NUMBER} failed", "FAILURE")
            }
        }
    }
}