# Mini-Project-2-enterprise-integration-patterns

The main objective of this project is to improve practical experience in implementing integrating enterprise applications by use of integration platforms and middleware.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites
Software needed for running this project

- Install RabbitMQ (https://www.rabbitmq.com/download.html)
- Install Visual studio 2019 (https://visualstudio.microsoft.com/downloads/)

### Setup Instructions
- Download this repository
- Open both the client and server with Visual studio 2019
- Change file paths to the direction your project is installed (There are 2 paths to adjust, both located in Receive.cs)
	- Mini-Project-2-enterprise-integration-patterns -> MiniProject2Server -> Receive.cs
- Start RabbitMQ
- Run Server and the Client afterwards
	
## BPMN (Business Procedure)
**Take a look at our BPMN model to understand the functionality of our program.**
As you see on our BPMN model we have 3 layers **Application A**, **Application B**, **Database**, the idea is that the Client _(App A)_ is asked alot of questions regarding details of a car, which is send and handled by the server _(App B)_ and at the end the result is that the client has made a booking which is stored in a file.

![BPMN model](BPMN/BPMNModel.JPG)	
	
## Intergration Patterns we cover

### Message Channel
![Request Reply](https://www.enterpriseintegrationpatterns.com/img/MessageChannelSolution.gif)
### Message
![Request Reply](https://www.enterpriseintegrationpatterns.com/img/MessageSolution.gif)
### Splitter
![Request Reply](https://www.enterpriseintegrationpatterns.com/img/Sequencer.gif)
### Aggregator
![Request Reply](https://www.enterpriseintegrationpatterns.com/img/Aggregator.gif)
### Request-Reply
![Request Reply](https://www.enterpriseintegrationpatterns.com/img/RequestReply.gif)
