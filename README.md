# 6. Azure Function
## Scheduled & HTTP Trigger

Azure Functions are hosted on a serverless infrastructure.

> Serverless computing is the abstraction of servers, infrastructure, and operating systems. When you build serverless apps you don’t need to provision and manage any servers, so you can take your mind off infrastructure concerns. 

#### a. `DoSomething`
The `DoSomething` function wakes up on a schedule and... well... does something. It simply logs out a message indicating when it was executed. No very exciting, but certainly does what it was written to do.

#### b. `TellJoke`

The `TellJoke` function is a little more fun. It leverages the "Internet Chuck Norris Database" to randomly return a joke for a named individual.

__Example Output__
```yaml
Hey, Dave... I have a joke for you. Chuck Norris's keyboard has the Any key.
```
