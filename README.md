# CV Guard API

The **CV Guard API** is a robust and secure API designed to handle the uploading and downloading of curriculum vitae (CV) files. It integrates with Azure Blob Storage for file storage, Postmark for email notifications, and includes features like IP-based location tracking and API key authentication. This API is built using ASP.NET Core and leverages various libraries and services to ensure reliability and security.

## Features

- **File Upload**: Upload CV files with a unique API key generated for each upload.
- **File Download**: Securely download uploaded CV files using the generated API key.
- **Email Notifications**: Send email notifications with the CV file as an attachment using Postmark.
- **IP-Based Location Tracking**: Track the location of the user based on their IP address.
- **Validation**: Validate upload and email requests using FluentValidation.
- **AutoMapper**: Automatically map between DTOs and models for cleaner code.
- **Azure Blob Storage**: Store uploaded files securely in Azure Blob Storage.
- **Postmark Integration**: Send templated emails with attachments via Postmark.

## Prerequisites

Before you begin, ensure you have the following:

- **.NET 8 SDK** or later installed.
- **Azure Storage Account** with a container for storing files.
- **Postmark Account** for sending emails.
- **Postmark API Key** and **Sender Email** configured in `appsettings.json`.

## Installation

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/devseoka/CV-Guard-Api.git
   cd cv-guard-api