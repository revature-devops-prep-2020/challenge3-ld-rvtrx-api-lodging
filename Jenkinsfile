void setBuildStatus(String message, String state) {
  step([
      $class: "GitHubCommitStatusSetter",
      reposSource: [$class: "ManuallyEnteredRepositorySource", url: "https://github.com/my-org/my-repo"],
      contextSource: [$class: "ManuallyEnteredCommitContextSource", context: "ci/jenkins/build-status"],
      errorHandlers: [[$class: "ChangingBuildStatusErrorHandler", result: "UNSTABLE"]],
      statusResultSource: [ $class: "ConditionalStatusResultSource", results: [[$class: "AnyBuildResult", message: message, state: state]] ]
  ]);
}

def repoName = "reblank/challenge3_lodging"
def versionTag = "1.0.0"
def kubeMaster = "https://my-cluster-challenge3-aks-f6d8e5-79c4b462.hcp.eastus.azmk8s.io"


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
                            sh 'dotnet publish -o app --no-restore --no-build'
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
        /*
        stage('Docker')
        {
            stages {
                stage('docker build') {   
                    agent {
                        docker { image 'docker' }
                    }
                    steps {
                        script {
                            image = docker.build(repoName)
                        }
                        sh "docker tag ${repoName} ${repoName}:${versionTag}"
                    }
                    post
                    {
                        success{
                            slackSend(color: '#00FF00', message: "${env.JOB_NAME}'s Docker image built successfully")
                        }
                        failure{
                            slackSend(color: '#FF0000', message: "${env.JOB_NAME}'s Docker image failed to build.")
                        }
                    }
                }
                stage('trivy') {
                    agent {
                        docker { 
                            image 'aquasec/trivy'
                            args '--net=host -v /var/run/docker.sock --entrypoint=' }
                    }
                    steps {
                        sh 'trivy image ' + "${repoName}:${versionTag}"
                    }
                    post
                    {
                        success{
                            slackSend(color: '#00FF00', message: "${env.JOB_NAME}'s trivy scan was successful")
                        }
                        failure{
                            slackSend(color: '#FF0000', message: "${env.JOB_NAME}'s trivy scan was unsuccessful")
                        }
                    }
                }
                
                stage('docker push') {
                    agent {
                        docker { image 'docker' }
                    }
                    steps {
                        script {
                            image = docker.image(repoName)
                            docker.withRegistry("", "docker_hub_credentials") {
                                image.push(versionTag)
                                image.push("latest")
                            }
                        }
                    }
                    post
                    {
                        success{
                            slackSend(color: '#00FF00', message: "${env.JOB_NAME}'s Docker image pushed to Docker Hub")
                        }
                        failure{
                            slackSend(color: '#FF0000', message: "${env.JOB_NAME}'s Docker image failed to push.")
                        }
                    }
                }
            }
            
        }

        stage('app deploy') {
            agent {
                docker { 
                    image 'reblank/kubectl_agent' 
                    args '--net=host'}
            }
            steps
            {
                git url: 'https://github.com/revature-devops-prep-2020/challenge3-ld-rvtrx-api-lodging', branch: "main"
                withKubeConfig([credentialsId: 'kube-sa', serverUrl: "${kubeMaster}"]) {
                    sh 'kubectl apply -f kubernetes'
                }
            }
            post
            {
                success{
                    slackSend(color: '#00FF00', message: "${env.JOB_NAME} deployed to cluster.")
                }
                failure{
                    slackSend(color: '#FF0000', message: "${env.JOB_NAME} failed to deploy.")
                }
            }
        }*/
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