# Social Network Exercise 
 
This is a console-based social networking application based on Twitter satisfying the scenarios below:

  - Posting: Publish a message to a personal timeline
  - Reading: View user's timelines
  - Following: Subscribe to user's timelines to be able to view an aggregated list of all subscriptions.
  - Wall: View an aggregated list of all following user's post and current user's post.
  
# Example 

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
