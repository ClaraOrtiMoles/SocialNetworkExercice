# Social Network Exercise 
 
This is a console-based social networking application based on Twitter satisfying the scenarios below:

  - Posting: Publish a message to a personal timeline
  - Reading: View user's timelines
  - Following: Subscribe to user's timelines to be able to view an aggregated list of all subscriptions.
  - Wall: View an aggregated list of all following user's post and current user's post.

## Main instructions

The first message received after run the application is a Welcome message, it gives us some instructions about how to use the application with the sintax of the commands. 
It's very important write the command properly since the program has been focused on the "sunny days scenario" and it returns few messages if the command is not recognized

* The user will be created after the first post: To post a message the sintaxis is: <username> -> <message>. If the username doesn't exist, the program will create the user and will store the message. 
* To read the posts of a specific user, the sintaxis is <username>. In case that the username doesn't exist, the program will show a message pointing out that the user doesn't exist.
* To follow a user it's necessary write the following sintaxis: <username1> follows <username2>. In case that one of those usernames doesn't exist, the program will return a message indicating wich is the user that hasn't been reached.
* To see the wall, meaning all the user's post and the following's post, the sintaxis is: <username> wall .Again, in case that the username doesn't exist the program will show a message. 
* To exit write: exit  

## Example 

    > Alice -> I love the weather today
    > Bob -> Damn! We lost!
    > Bob -> Good game though.
    
    > Alice 
    I love the weather today (5 minutes ago)
    > Bob
    Good game though. (1 minute ago)
    Damn! We lost! (2 minutes ago)
    
    > Charlie -> I'm in New York today! Anyone want to have a coffee?
    > Charlie follows Alice
    > Charlie wall
    Charlie - I'm in New York today! Anyone want to have a coffee? (2 seconds ago)
    Alice - I love the weather today (5 minutes ago)
    
    > Charlie follows Bob
    > Charlie wall
    Charlie - I'm in New York today! Anyone want to have a coffee? (15 seconds ago)
    Bob - Good game though. (1 minute ago)
    Bob - Damn! We lost! (2 minutes ago)
    Alice - I love the weather today (5 minutes ago)

## Getting Started

Download or fork this repository.

### Prerequisites

Visual studio 2015 or above, .NET Framework 4.5.2, NuGet Packages v3 

### Installing

* Open solution **SocialNetworkExercise.sln** via Visual Studio 2017.
* Set SocialNetworkExercise as **StartUp project**
* **Build** solution: The NuGet Packages should be automatically installed or updated after the first build. 
* **Run** the solution. In that point you could run it using the debugger (F5) or without using it (Ctrl + F5). 

## Development points

In order to develop this exercise, I have try to deal between the real life project and the exercise. Meaning, I have been focused in the exercise as it was a real project but trying to don't over architecture the application. 

Probably this has been the most difficult part, because some of the classes or services created are quite small, and probably they could be created into other related module, such as DataService that the only thing it does is create or get the user using an UserName, so it could be done into CommandService module, but in a "real" big project, we should have all related with data acces, in a layer in order to avoid dependencies and be able to change the data source if it's needed without change all the logic and also doing test work more easy. 

But since our program is working in memory, it has been imposible to isolate the data acces completely. 

Although the main idea has been to try to develop the application like it would be a real project, I have been completely focused in try to abstract the code enough in order to be able to add new commands with the less impact possible. 

### Architecture
From an architectural point of view, this project has "three layers" (but in order to not complicate the exercice I have maintain all the code in the application layer, but I have created three services with their contract that would identify each layer). 
* The first layer would be the application layer. In the project, Program.cs is the main class, meaning, the point of entry. What Program.cs does is to configure all the dependencies (Configuration.cs), read and write from the console, decrypt the message from the console, and call to action.
To this purpose, the application layer uses IConsoleService, which has the four methods: ConvertMessageToCommand, ExecuteCommand, Read and Write.
* The second layer would be the business layer. The business layer would be the actions that the program should do when the application says that a specific command has to be executed. This actions are coded on ICommandService, and basically what this service does is bring the four commands that currently the program support: Post, Read, Follow, Write. 
* Finally, the third layer would be the data layer. This layer would be in charge to execute the CRUD operations. Since this project is in memory and we are not using any storage for the data, this layer has only the methods more isolatables like getUser, createUser, userExist.  

### Open to change
As I mentioned one of the biggest point has been to be opened to change. 

### Practices used 

## Running the tests

Attached with the solution, there is **SocialNetworkExercice.Test** project. This project contains a few unit tests to test the different modules of the project. 

### Break down into end to end tests





## What about a real life project

## Author 

**Clara Orti Moles** 
[@ClaraOrtiMoles](https://twitter.com/ClaraOrtiMoles)


