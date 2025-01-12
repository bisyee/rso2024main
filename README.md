# rso2024main

# **RSO Project 2024**

Welcome to the **RSO Project 2024** repository! This project serves as a full-stack application, combining frontend, backend, and database services to create a smart parking system.

## **Project Structure**

The repository contains the following main directories:

- **`.github/workflows`**: Contains CI/CD pipeline configurations.
- **`SharedModels`**: Shared models used across services.
- **`backend`**: Backend services for the application.
- **`frontend`**: Frontend services for the application.
- **`db`**: Database configuration files.
- **`kubernetes`**: Kubernetes deployment and service configurations.
- **Other Files**:
  - `docker-compose.yml`: Docker Compose configuration for local setup.
  - `README.md`: Documentation file (this file).

## **Technologies Used**

- **Backend**: .NET Core (C#)
- **Frontend**: Blazor
- **Database**: PostgreSQL
- **Containerization**: Docker
- **Orchestration**: Kubernetes
- **CI/CD**: GitHub Actions

---

## **Getting Started**

### **Prerequisites**

Ensure you have the following installed:

- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)
- [Kubernetes (kubectl)](https://kubernetes.io/docs/tasks/tools/)
- [.NET SDK](https://dotnet.microsoft.com/)
- [Node.js](https://nodejs.org/)
- [.NET](https://learn.microsoft.com/en-us/dotnet/core/install/windows))
- PostgreSQL client (e.g., `psql`)

---

### **Installation**

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/bisyee/rso2024main.git
   cd rso2024main
   ```

2. **Setup Environment Variables**:
   Update any required environment variables in the `docker-compose.yml` file and Kubernetes YAML files.

3. **Run Services with Docker Compose**:
   ```bash
   docker-compose up --build
   ```

4. **Access the Application**:
   - Frontend: `http://localhost:80`
   - Backend: `http://localhost:8080`
   - Database: `localhost:5432`

---

## **Usage**

### **Accessing the Database**
Connect to the database using the following command:
```bash
psql -h localhost -U postgres -d SmartParkDatabase
```
Credentials:
- **Username**: `postgres`
- **Password**: `turisticka`

### **Kubernetes Deployment**
1. Deploy services to a Kubernetes cluster:
   ```bash
   kubectl apply -f kubernetes/
   ```
2. Verify the deployments:
   ```bash
   kubectl get pods
   kubectl get services
   ```

---

## **Development**

### **Backend Development**
Navigate to the `backend` directory and run the following:
```bash
dotnet run
```

### **Frontend Development**
Navigate to the `frontend` directory and run the following:
```bash
dotnet watch run
```

---

## **CI/CD**
The repository uses GitHub Actions for CI/CD. The pipeline is configured in `.github/workflows/cd-pipeline.yml`.

---

## **Contributing**
Contributions are welcome! Please fork the repository and submit a pull request for any changes.

---


## **Contact**
For any questions or suggestions, please contact:

**Bisera Nikoloska**  
[bisera.nikoloska@hotmail.co.uk](mailto:bisera.nikoloska@hotmail.co.uk)

**Ljubica Simovska**  
[simovskabube@gmail.com](mailto:simovskabube@gmail.com)
