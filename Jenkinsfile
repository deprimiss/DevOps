pipeline {
    agent any

    stages {
        stage('Настройка инфраструктуры') {
            steps {
                echo 'Выполнение настройки инфраструктуры (Установка Docker и получение исходного кода)...'
                withCredentials([usernamePassword(credentialsId: 'deprimiss', usernameVariable: 'deprimiss', passwordVariable: 'qwerty')]) {
                    sh '''
                        export ANSIBLE_CONFIG=${WORKSPACE}/ansible/ansible.cfg
                        cd ansible
                        playbook.yml \
                            -i inventory/hosts \
                            -t install,clone \
                            -u ${deprimiss} \
                            --extra-vars "ansible_ssh_pass=${qwerty} ansible_become_pass=${qwerty}"
                    '''
                }
            }
        }

        stage('Развертывание сервисов') {
            steps {
                echo 'Развертывание сервисов приложения (Docker Compose up)...'
                withCredentials([usernamePassword(credentialsId: 'deprimiss', usernameVariable: 'deprimiss', passwordVariable: 'qwerty')]) {
                    sh '''
                        export ANSIBLE_CONFIG=${WORKSPACE}/ansible/ansible.cfg
                        cd ansible
                        playbook.yml \
                            -i inventory/hosts \
                            -t start \
                            -u ${deprimiss} \
                            --extra-vars "ansible_ssh_pass=${qwerty} ansible_become_pass=${qwerty}"
                    '''
                }
            }
        }
    }
    
    post {
        success {
            echo 'CI/CD пайплайн завершен успешно!'
        }
        failure {
            echo 'Выполнение пайплайна завершилось с ошибкой.'
        }
    }
}

