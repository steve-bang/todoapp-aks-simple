echo "Setup enviroment for project."

#!/bin/bash

echo "1. Setup deployement"

echo "1.1 Create volume"
kubectl apply -f ./k8s/pg-pv.yaml
echo "Apply postgres pv success!"

echo "1.1 Start apply pg.yaml..."
kubectl apply -f ./k8s/pg.yaml
echo "Apply postgres database development success!"

echo "1.2 Start apply pg-secret.yaml..."
kubectl apply -f ./k8s/pg-secret.yaml
echo "Apply postgres secret success!"

echo "1.3 Start apply pg-configmap.yaml..."
kubectl apply -f ./k8s/pg-configmap.yaml
echo "Apply postgres config map success!"

echo "1.4 Start apply pgadmin.yaml..."
kubectl apply -f ./k8s/pgadmin.yaml
echo "Apply postgres admin success!"


echo "1.5 Start apply todoapp-config.yaml..."
kubectl apply -f ./k8s/todoapp-configmap.yaml
echo "Apply todo app configmap success!"


echo "2 Deploy webapi..."
kubectl apply -f ./k8s/todoapp-api.yaml
echo "Apply todo app todo app api success!"

echo "3 Deploy webapp..."
kubectl apply -f ./k8s/todoapp-web.yaml
echo "Apply todo app todo app web success!"