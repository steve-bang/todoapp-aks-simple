
# TodoApp AKS Simple project
This project is used for integration Azure Kubernet Service.



# Tentative technologies and frameworks
- .NET 9
- ReactJS 18
- K8s
- Azure Kubernetes Service
- Postgresql

# Getting started


1. Create Resource Group
``` bash
az group create \
    -l <your location> \
    --name todoapp-rg
```


2. Create AKS
``` bash
az aks create \
    --resource-group todoapp-rg \
    --name todoapp-cluster \
    --node-count 1 \
    --generate-ssh-keys
```

3. Connect todoapp-cluster node from local

``` bash
az aks get-credentials --resource-group todoapp-rg --name todoapp-cluster
```

4. Check result via cli or portal
``` bash
kubectl get all
kubectl get node
```

5. Apply all config with just run

``` bash
./setup.sh
```

6. Check result
After apply all config success, you must be go to the azure portal and visit on your cluster and see your external IP on your service that is belong to LoadBalancer type. You can take them and past the external IP adress of BE service for webapp project with set `REACT_APP_API_BASE_URL=http://<Your-external-ip>`

7. Apply ingress and mapping cloud flare domain

Before mapping cloud, you must be create an ingree
``` bash
kubectl apply -f ./k8s/dashboard-ingress.yaml
```

Check result
``` bash
kubectl get ingress
```

Install Cert-manager in your cluster:
``` bash
kubectl apply -f https://github.com/cert-manager/cert-manager/releases/download/v1.9.1/cert-manager.yaml
```

Configure Cloudflare DNS for your domain
1. Log in to your Cloudflare Dashboard.
2. Select your domain from the dashboard.
3. Go to the DNS tab.
4. Add a new A record:
- Type: A
- Name: subdomain (e.g., app for app.yourdomain.com)
- IPv4 Address: Your ingress controller's public IP (LoadBalancer IP if using a cloud service).
- Set Proxy Status to Proxied (orange cloud) if you want Cloudflare's protection or DNS only (gray cloud) if you do not want it.
5. Alternatively, create a CNAME if you are pointing to another domain:
- Type: CNAME
- Name: subdomain
- Target: your.ingress-controller-domain.com

# Kubernetes Cluster Communication Overview

This document explains the communication flow within the Kubernetes cluster using the provided configuration files.

---

## 1. ConfigMap and Secrets

### ConfigMap (`postgres-secret`)
- Provides environment variables like:
  - `POSTGRES_DB`
  - `POSTGRES_USER`
  - `POSTGRES_PASSWORD`
- These variables are used to configure the Postgres database during initialization.

### Secret (`postgres-secret`)
- Contains a Base64-encoded connection string:
- Consumed by `todoapp-api` for database connectivity.

---

## 2. Persistent Storage

### PersistentVolume (PV) and PersistentVolumeClaim (PVC)
- **Postgres**:
- `postgres-pv` and `postgres-pvc` provide durable storage for Postgres data.
- **Storage Classes**:
- `db-storage` and `high-speed-file-storage` allow dynamic provisioning of storage resources.
- Ensures data persistence across pod restarts.

---

## 3. Networking and Services

### Postgres Service (`postgres-service`)
- **Type**: `NodePort`
- Exposes the Postgres database on port `5432`.
- Stable network identity for Postgres pods, enabling communication from other pods (like `todoapp-api`).

### PgAdmin Service (`pgadmin-service`)
- **Type**: `LoadBalancer`
- Exposes PgAdmin for external access on port `80`.
- Provides a web-based database management interface.

### TodoApp API Service (`todoapp-api-service`)
- **Type**: `LoadBalancer`
- Exposes the API backend to external users.
- Routes traffic on port `80` to the `todoapp-api-container`.

### TodoApp WebApp Service (`todoapp-webapp-service`)
- **Type**: `LoadBalancer`
- Exposes the web application to external users.
- Routes traffic on port `80` to the `todoapp-webapp-container`.

---

## 4. Inter-Service Communication

### `todoapp-api` to Postgres
- Uses the `ConnectionStrings__DefaultConnection` environment variable:
- Configured using `config.js` mounted from `todoapp-config`.

---

## 5. Internal Pod-to-Pod Communication

Pods communicate internally using **DNS-based service discovery**:
- Database access: `postgres-service:5432`
- API access: `todoapp-api-service:80`

### Example Flow
1. `todoapp-api` connects to `postgres-service` for database queries.
2. `todoapp-webapp` sends requests to `todoapp-api-service` for backend API functionality.

---

## 6. Scaling and Load Balancing

### Deployments
- **Postgres**:
- Configured with `replicas: 3` for high availability.
- **TodoApp API and WebApp**:
- Each starts with one replica but can scale based on traffic.

### Load Balancing
- Services (e.g., `postgres-service`) distribute traffic across pod replicas automatically.

---

## 7. Security Considerations

- **Secrets**:
- Used for sensitive data like database connection strings.
- **ConfigMaps**:
- Store non-sensitive configuration data (e.g., database names, usernames).
---

## Summary

- **Services** provide stable network endpoints for pod communication.
- **DNS-based resolution** enables seamless internal connectivity.
- **Persistent storage** ensures data durability.
- **Secrets and ConfigMaps** secure and manage configurations effectively.
- The cluster is designed for scalability and high availability, with load balancing handled by Kubernetes services.

## Destroying All Development Resources and Deleting the Cluster
To delete all resources defined in your configuration files, run the following command
``` bash
az group delete -n todoapp-rg
```

In azure, I just delete resource group, all of them will be delete. 
If you want to delete step by step, you can run the following command
``` bash
kubectl get all
```

This command will show you all the component running your cluster. 
You can delete any component if you want, You should be delete deployment.