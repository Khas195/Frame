EXTERNAL OpenTaxiMap()
-> Conversations.TaxiDriver
=== Conversations ===

= TaxiDriver 

Hey, Ya want to do some wher' pal !?
    * Yup, Let me see the map.
    * No, just passing by.
    * I don't know yet, I have some questions.
    I'm a just a normal taxi driver, not a infomation lady!.
        * * Don't be a mackerel now!.
        * * Just some common questions, no need to be so annoyed.
            - - - -> TaxiDriverQuestions
            
        * * You know what, never mind, I'll be on my way.
    - See you around then.
    -> DONE
= TaxiDriverQuestions        
Alright, what do you want to ask ?.
    * * Where can you take me ?
        Right now, not a lot of places, the dev team is not done making them yet.
        * * * What 'dev' team ?
        * * * What are you talking about ?
        * * * Huh ?
        - - - Well, of course you won't get it. I'm not talking to you anyway... But congrats to us for being test subjects for the dialogue system!.
                * * * * ..
                -> TaxiDriverQuestions
    * * So, how you doing ?
        Look Pal, me and you just met, we're not there yet, are you going somewhere or not ?.
        * * * What a Mackerel!
        * * * Don't have to be so rude!.
        - - - Whatever man!.
        -> TaxiDriverQuestions
    * * Nothing really!.
        ... ... What's wrong with you!.
-> DONE


-> END