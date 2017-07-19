# Social Network Exercise 
 
This is a console-based social networking application based on Twitter satisfying the scenarios below:

  - Posting: Publish a message to a personal timeline
  - Reading: View user's timelines
  - Following: Subscribe to user's timelines to be able to view an aggregated list of all subscriptions.
  - Wall: View an aggregated list of all following user's post and current user's post.

## Main instructions

The first message received after run the application is a Welcome message, it gives us some instructions about how to use the application with the sintax of the commands. 
It's very important write the command properly since the program has been focused on the "sunny day scenarios" and it returns few messages if the command is not recognized

* The user will be created after the first post: To post a message we should use:  ``<username> -> <message>`` . If the username doesn't exist, the program will create the user and will store the message. 
* To read the posts of a specific user, the sintax is ``<username>`` . In case that the username doesn't exist, the program will show a message pointing out that the user doesn't exist.
* To follow a user it's necessary write the following sintaxis:  ``<username1>  follows <username2>`` . In case that one of those usernames doesn't exist, the program will return a message indicating wich is the user that hasn't been reached.
* To see the wall, meaning all the user's post and the followings' post, the sintax is: ``<username> wall`` , in case that the username doesn't exist the program will show a message. 
* To exit write: exit  

## Example 

    Welcome! Your user will be created after your first post.
    * To create a post write your username and your message with the following format <username> -> message
    * To read all the message from a user write the username
    * To follow an user use the following format <username> follows <username>
    * To see your wall write <username> wall
    * To exit write exit

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

    > exit
    Thank you. Bye.


## Getting Started

Download or fork and clone this repository. 

### Prerequisites

Visual studio 2015 or above, .NET Framework 4.5.2, NuGet Packages v3 

### Installing

* Open solution **SocialNetworkExercise.sln** via Visual Studio 2017.
* Set SocialNetworkExercise as **StartUp project**
* **Build** solution: The NuGet Packages should be automatically installed or updated after the first build. 
* **Run** the solution. In that point you could run it using the debugger (F5) or without using it (Ctrl + F5). 

## Development points

In order to develop this exercise, I have try to deal between the real life project and the exercise. Meaning, I have been focused in the exercise as it were a real project but trying to don't over architecture the application. 

Probably this has been the most difficult part, because some of the classes or services created are quite small, and probably they could be created into other related module, such as DataService the only thing it does is create or get the user using an UserName, so it could be done into CommandService module, but in a "real" big project, we should have all related with data acces in a layer in order to make easy the test work avoiding dependencies and be able to change the data source if it's needed without change all the logic.

But since our program is working in memory, it has been imposible to isolate the data acces completely. 

Although the main idea has been try to develop the application like it were a real project, I have been completely focused trying to abstract the code enough in order to be able to add new commands with the less impact possible. 

### Architecture
From an architectural point of view, this project has "three layers" (but in order to not complicate the exercice I have maintained all the code in the application layer, but I have created three services with their contract that would identify each layer). 
* The first layer would be the application layer. In the project, Program.cs is the main class, meaning, the point of entry. What Program.cs does is configuring all the dependencies (Configuration.cs), read and write from the console, decrypt the message from the console, and call to action.
To this purpose, the application layer uses IConsoleService, which has the four methods: ConvertMessageToCommand, ExecuteCommand, Read and Write.
* The second layer would be the business layer. The business layer would keep the actions that the program should do when the application commands to execute a specific command. This actions are coded on ICommandService, and basically what this service does is bringing the four commands that currently the program support: Post, Read, Follow, Write. 
* Finally, the third layer would be the data layer. This layer is in charge to execute the CRUD operations. Since this project is in memory and we are not using any storage for the data, this layer has only the methods more isolatables like getUser, createUser, userExist.  

### Open to change
As I mentioned one of the biggest point during the development of this project has been to maintain the principle "be opened to change". Meaning in the case the application wants to add a new command, let's say ``<username1> unfollows <username2>``. The steps would be:
* Add a new enum value in CommandEnum to identify the new command, i.e. CommandEnum.UnFollow 
* Add the key of the command into the Dictionary existing in the Service ConsoleService.cs this new key would be "unfollows" with the value of the Enum created. 
* Add a new contract method on ICommandService that returns an string and execute a Command with the data. Obviously implement this method. 
* Add a new entry into the dictionary CommandActions created in ConsoleService, with the CommandEnum.UnFollow as a key, and the action on commandService as a value. 
 
### Practices used 
Trying to use TDD

Since it is not a big project and it is not complicated either, I have been trying to follow the practice Test Drive Development to write the application. That means, first I wrote the test, and then I run the test and I created the code necesary to pass the test, and finally clean the code. 
I say "trying" because, it has been my first time with this practice, and I haven't been strict following it, though I am really committed to learn and hopefully in a few time I will craft it. 

## Running the tests

Attached with the solution, there is **SocialNetworkExercice.Test** project. This project contains a few unit tests to test the different modules of the project. 

Basically, it is possible to run the test using visual studio test explorer. 

### Break down into end to end tests

The test project contain three TestClasses:
* **CommandServiceUnitTest.cs** that contains the test methods for testing 4 commands that the application executes: Post, Wall, Follow, Read.
* **ConsoleServiceUnitTest.cs** contains the test methods for testing all related with read and decript message and commands..
* **DataServiceUnitTest.cs** contains the test methods for test the methods related with the creation and reading of the users.


### Info useful about the test

The main and first issue that I experienced developing the tests, was when the command implies to show a post, the return string is 
a combination of a message and the time ago, obviously this time ago it will be diferent everytime that we will run the test because the Time in the class Post is private as we can not modify the time when the post has been written, and the time ago is the difference between the current time and the post time. 

What I've done is to suppose that the processor is enough fast to run everything between the first minute, and I've been playing with Sleep in order to make the post a bit more spaciaced temporay and finally I calculate the difference between DateTime.Now and the time of the post assuming that it always will be between the first minute so the time ago will be in seconds. Sometimes it fails, because sometimes it is not the first minute, but most of the case it works and it is quite correct. 

I know that in a real project that wouldn't be acceptable, because if the deploy depends of the unit tests and the unit tests fail, it could generate a lot of issues. But in the small scope of this application, I've decided that this fix is enough to our purpose. 

I am looking forward to learn how to deal with situations like this. 

## Author 

**Clara Orti Moles** 
[@ClaraOrtiMoles](https://twitter.com/ClaraOrtiMoles)


