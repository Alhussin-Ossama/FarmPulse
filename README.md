# 🐔 FarmPulse - Smart Poultry Monitoring API

FarmPulse is an intelligent poultry monitoring system designed to **track chicken weight and activity** in real time using **ESP32, load cell sensors, and RFID tags**. The system detects abnormalities in weight gain, sends automated notifications, and provides detailed statistical insights.

## 🚀 Features

- **📡 Real-time Weight Monitoring**: Continuously tracks chicken weight throughout the day.
- **⚠️ Health Alerts**: Sends **notifications via website or mobile** when a chicken shows potential health issues.
- **📊 Farm Statistics**: Generates **daily, weekly, and yearly** reports for:
  - 🏋️‍♂️ **Average weight**
  - 💀 **Mortality rate**
  - 🐔 **Survival rate**
- **🔑 Secure User Authentication**: Implements **JWT Authentication** and **Role-Based Access Control (RBAC)**.
- **📈 Data Visualization Support**: Enables graphical representation of farm statistics.
- **🛠️ Modular & Scalable Architecture**: Built using **Onion Architecture**, following best practices in **Repository & Unit of Work** patterns.
- **📝 API Documentation**: Integrated **Swagger UI** for easy testing and reference.

## 🛠️ Tech Stack

- **Backend**: ASP.NET Core Web API, Entity Framework Core
- **Database**: SQL Server
- **Hardware**: ESP32, Load Cell (5kg), PN532 RFID Module
- **Authentication**: JWT Tokens, ASP.NET Identity
- **Frontend (Planned Integration)**: HTML, CSS, JavaScript

## 📜 API Endpoints

### 🔐 Authentication
| Method | Endpoint | Description |
|--------|---------|-------------|
| `POST` | `/api/accounts/register` | Register a new user |
| `POST` | `/api/accounts/login` | Authenticate and get a JWT token |
| `GET`  | `/api/accounts/getcurrentuser` | Retrieve the logged-in user details |

### 🐔 Chicken Management
| Method | Endpoint | Description |
|--------|---------|-------------|
| `POST` | `/api/chicken/addOrUpdateChickenWithWeight` | Add or update chicken weight |
| `GET`  | `/api/chicken/getAllChickens` | Retrieve all chickens |
| `GET`  | `/api/chicken/getChickensByStatus/{status}` | Filter chickens by status (Alive, Sick, Dead) |
| `PUT`  | `/api/chicken/{RFID}/{newStatus}` | Update a chicken’s activity status |
| `DELETE` | `/api/chicken/{RFID}` | Delete a chicken record |

### ⚖️ Weight & Statistics
| Method | Endpoint | Description |
|--------|---------|-------------|
| `GET`  | `/api/statistics/getStatistics/{period}` | Retrieve farm statistics for a given period (daily, weekly, yearly) |

### 🔔 Notifications
| Method | Endpoint | Description |
|--------|---------|-------------|
| `GET`  | `/api/notifications/getAllNotifications` | Retrieve all notifications |
| `PUT`  | `/api/notifications/markAsRead/{id}` | Mark a notification as read |
| `DELETE` | `/api/notifications/deleteNotification/{id}` | Delete a notification |

## 📦 Installation & Setup

### 1️⃣ Clone the Repository  
```sh
git clone https://github.com/Alhussin-Ossama/FarmPulse.git
cd FarmPulse/FarmPulse.API
```

### 2️⃣ Configure the Database  
Update `appsettings.json` with your **SQL Server connection string**.

### 3️⃣ Apply Migrations & Seed Data  
```sh
dotnet ef database update
```

### 4️⃣ Run the API  
```sh
dotnet run
```

### 5️⃣ Access API Documentation  
Open your browser and go to:  
🔗 [`http://localhost:port/swagger`](http://localhost:port/swagger)

---

## 🔮 Future Enhancements  
- ☁ **Cloud Deployment** (Azure / AWS)  
- 📊 **Enhanced Data Visualization with Graphs**  
- 📱 **Mobile App for Real-time Monitoring**  
- 🤖 **AI-powered Health Predictions**  

---

## 🤝 Contributing  
Contributions are welcome! Feel free to **fork** the repository, **raise issues**, or submit **pull requests**. 🚀  

## 📧 Contact  
📩 **Email**: [hussinossama44@gmail.com](mailto:hussinossama44@gmail.com)  
🔗 **GitHub**: [FarmPulse Repository](https://github.com/Alhussin-Ossama/FarmPulse)  

