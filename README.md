# Parking Systems

a rate calculation engine for a carpark, the inputs for this engine are:
1. Patron’s Entry Date and Time
2. Patron’s Exit Date and Time

Based on these 2 inputs the engine program should calculate the correct rate for the patron and display the name of  the rate along with the total price to the patron using the following rates: 

Early Bird
  - Flat Rate
  - $13.0
  - Entry condition : Enter between 6:00 AM to 9:00 AM
  - Exit condition : Exit between 3:30 PM to 11:30 PM
   
Night Rate
  - Flat Rate
  - $6.50
  - Entry condition : Enter between 6:00 PM to midnight (weekdays)
  - Exit condition : Exit before 6 AM the following day
   
Weekend Rate
  - Flat Rate
  - $10.00 
  - Entry condition : Enter anytime past midnight on Friday to Sunday
  - Exit condition : any time before midnight of Sunday

Note:
If a patron enters the carpark before midnight on Friday and if they qualify 
for Night rate on a Saturday morning, then the program should charge the 
night rate instead of weekend rate. 
  
## Standard Rate

| Type | Hourly Rate |
| ------ | ------ |
| 0 - 1 | $5.00|
| 1 - 2 | $10.00|
| 2 - 3 | $15.00|
| 3+ | $20.00 * flat rate per day for each day of parking.|
