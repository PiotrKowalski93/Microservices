kubectl apply -f local-pvc.yaml
kubectl apply -f mssql-plat-depl.yaml

kubectl apply -f rabbitmq-depl.yaml

kubectl apply -f platformsApi-deploy.yaml
kubectl apply -f commandsApi-deploy.yaml