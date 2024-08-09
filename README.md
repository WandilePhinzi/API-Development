[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/290U_JNB)

# NWU Tech Trends API

## Project 2 Overview
* This project aims to develop a CRUD( Create ,Read ,Update, Delete) RESTful API so that it can manage telemetry
data associated with automations created by NWU Tech Trends. The API will connect to the database stroring this 
telemetry information and allow functionalities like retrieving all telemetry entiries retreiving a specific telemetry
entry withing ID, creating new telemetry entried. Updating ecisting telemeetry entries , deleting telemetry entries
and calculating cumulative time and cost saved for projects and clients based on data ranges.The primary goal is to provide insights into time and cost savings generated by these automations.
* The API is designed to be hosted on Azure and provides secure access through authentication. Stakeholders can use the API to retrieve and manage telemetry data, and the accompanying report details the implementation, functionality, and usage of the API, including specific endpoint descriptions and access instructions. The report created is intended to guide users through APIs capabilities and help with intergration. This will ensure effective
utilization o the telemetry data for analyzing automation performance

## How to use the API
* Authentication: Ensure that you have all the necessary credentials to access the API
* Base URL https://nwutechtrends20240807130028.azurewebsites.net
* Endpoints:
* Telemetry Management
    - Get/api/jobtelemetry :This retreievs all telemetry entries
    - Get/api/jobtelemetry{id} : This Retrieves a single telemetry entry by its ID
    - Post/api/jobtelemetry: This creates a new telemetry entry
    - Patch/api/jobtelemetry{id}:Updates an existing telemetry entry
    - Delete/api/jobtelemetry{id}: Delete a telemetry entry
    - Get/api/jobtelemetry/GetSavings/project: Retrieves cumulative tima and cost savings griuped by project
    - Get/api/jobtelemetry/GetSavings/client :Retreives cumulative time and cost savings grouped by client



