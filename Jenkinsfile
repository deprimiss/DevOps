pipeline {
    agent any

    stages {
        stage('Настройка инфраструктуры') {
            steps {
                echo 'Выполнение настройки инфраструктуры (Установка Docker и получение исходного кода)...'
                withCredentials([usernamePassword(credentialsId: 'ansible-ssh-user', usernameVariable: 'SSH_USERNAME', passwordVariable: 'SSH_PASSWORD')]) {
                    sh '''
                        export ANSIBLE_CONFIG=${WORKSPACE}/ansible/ansible.cfg
                        cd ansible
                        ansible-playbook playbook.yml \
                            -i inventory/hosts \
                            -t install,clone \
                            -u ${SSH_USERNAME} \
                            --extra-vars "ansible_ssh_pass=${SSH_PASSWORD} ansible_become_pass=${SSH_PASSWORD}"
                    '''
                }
            }
        }

        stage('Развертывание сервисов') {
            steps {
                echo 'Развертывание сервисов приложения (Docker Compose up)...'
                withCredentials([usernamePassword(credentialsId: 'ansible-ssh-user', usernameVariable: 'SSH_USERNAME', passwordVariable: 'SSH_PASSWORD')]) {
                    sh '''
                        export ANSIBLE_CONFIG=${WORKSPACE}/ansible/ansible.cfg
                        cd ansible
                        ansible-playbook playbook.yml \
                            -i inventory/hosts \
                            -t start \
                            -u ${SSH_USERNAME} \
                            --extra-vars "ansible_ssh_pass=${SSH_PASSWORD} ansible_become_pass=${SSH_PASSWORD}"
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

