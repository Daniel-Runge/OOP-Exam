# OOP Exam Project
This document contains my analysis and design choices for the Object Oriented Programming (OOP) course examination.

## The Task
To produce a CLI system for the F-club, to manage the *Users*, *Products*, and *Transactions* between these.
Compared to the original system, which is being improved upon here, the long awaited 'multi-quickbuy' function will finally be implemented.

The task consists of four parts:
1. The core of the system and model
2. The user interface
3. Control of both
4. Composition of the above

### Part 1: The system core
Since the system has to manage *Users*, *Products*, and *Transactions*, we start by modelling these. 
Only uncertainties, interesting details or weird  are mentioned here. For full details, see the assignment pdf.

#### User
1. ID, typically this would be better managed through the database. for now it's simply handled as a static variable, updated everytime a new instance of the User class is created.
It might be prudent to add a secondary constructor with an ID parameter, for creating objects from a file.
2. First and last names just have to be not null here, but that will hardly be enough for a proper application.
3. Username and email, I have chosen to use regular expressions here. It's just simpler. There should probably be more requirements though, for example length.
In both cases the pattern for matching should not be placed inside the class, but rather as a configuration.
4. Balance, is the big issue here. Typically I would use ints for all kinds of monetary calculation, but alas it has to be decimal. Secondly it wants to use a delegate.
Where should a delegate type be declared? Where should it be used... What does it return? what's the purpose of the 'balance' parameter, since the user balance is already contained in the object.
Is it the limit for when the warning should be issued? Which function(s) need to take the delegate as a parameter...?

