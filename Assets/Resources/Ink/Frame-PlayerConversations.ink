EXTERNAL OpenTaxiMap()
-> Conversations.TaxiDriver
=== Conversations ===

= TaxiDriver 

Hey, Ya want to do some wher' pal !?
    * 1. Yup, Let me see the map.
    * 2. No, just passing by.
    * 3. I don't know yet, I have some questions.
    I'm a just a normal taxi driver, not a infomation lady!.
        * * 1. Don't be a mackerel now!.
        * * 2. Just some common questions, no need to be so annoyed.
        - - -> TaxiDriverQuestions
            
        * * 3. You know what, never mind, I'll be on my way.
    - See you around then.
    -> DONE
= TaxiDriverQuestions        
Alright, what do you want to ask ?.
    * * 1. Where can you take me ?
        Right now, not a lot of places, the dev team is not done making them yet.
        * * * 1. What 'dev' team ?
        * * * 2. What are you talking about ?
        * * * 3. Huh ?
        - - - Well, of course you won't get it. I'm not talking to you anyway... But congrats to us for being test subjects for the dialogue system!.
                * * * * 1. ..
                -> TaxiDriverQuestions
    * * 2. So, how you doing ?
        Look Pal, me and you just met, we're not there yet, are you going somewhere or not ?.
        * * * 1. What a Mackerel!
        * * * 2. Don't have to be so rude!.
        - - - 3. Whatever man!.
        -> TaxiDriverQuestions
    * * 3. Nothing really!.
        ... ... What's wrong with you!.
-> DONE


-> END