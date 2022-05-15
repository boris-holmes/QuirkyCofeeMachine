# QuirkyCoffeeMachine

This project provides an API that controls an imaginary quirky internet-connected coffee machine. I didn't bother with any authentication, it's free coffee for everyone !

## Test cases
I have decided to only do manual integration tests on the business logic. I am also not testing the data persistence and network aspect, I will assume that they work.

### Once the coffee machine is empty (503), it's refilled and ready to make coffee
- Pre-requisite:
--- The coffee machine is online
--- It's not the 1st of April
- Query the endpoint until you get a 503 response code
-> The next response should be a 200 response code and you should have a coffee

### I can get 5 brews out of a single coffee bean fill
- Pre-requisite:
--- The coffee machine is online
--- It's not the 1st of April
- Query the endpoint until you get a 503 response code
- Query the endpoint 5 times
-> you should get 200 response codes and get a coffee every time
- Query the endpoint
- > You should get a 503 response code and not get a coffee

### The coffee machine makes a joke on 1st of April
- Pre-requisite:
--- The coffee machine is online
--- Make it to be the 1st of April (change the coffee machine date)
- Query the endpoint
- > You should get a 418 response code and no coffee

### The 1st of April behaviour does not interfer with the number of brews
### I can get 5 brews out of a single coffee bean fill
- Pre-requisite:
--- The coffee machine is online
--- It's not the 1st of April
- Query the endpoint until you get a 503 response code
- Query the endpoint 2 times
- Change the date on the coffee machine to the 1st of April
- Query the endpoint
- Change the date back to normal
- Query the endpoint 3 times
- > You should get 200 response codes and a coffee everytime
- Query the endpoint
-> You should get a 503 response code and not get a coffee
