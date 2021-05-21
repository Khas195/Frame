EXTERNAL OpenTaxiMap()
EXTERNAL OpenNewsPanel()

-> Conversations.TaxiDriver
=== Conversations ===

= TaxiDriver 

Hey, Ya want to do some wher' pal !?
    + "Yup, Let me see the map."
    ~ OpenTaxiMap()
    -> END
    + "No, just passing by."
    + "I don't know yet, I have some questions."
    I'm a just a normal taxi driver, not a infomation lady!.
        + + "Don't be a mackerel now!."
            - - - -> TaxiDriverQuestions
        + + "Just some common questions, no need to be so annoyed."
            - - - -> TaxiDriverQuestions
            
        + + "You know what, never mind, I'll be on my way."
    - See you around then.
    + End
    -> DONE
= TaxiDriverQuestions        
Alright, what do you want to ask ?.
    + + "Where can you take me ?"
        Right now, not a lot of places, the dev team is not done making them yet.
        + + + "What 'dev' team ?"
        + + + "What are you talking about ?"
        + + + "Huh ?"
        - - - Well, of course you won't get it. I'm not talking to you anyway... 
        + + + Continue
        - - - But congrats to us for being test subjects for the dialogue system!.
                + + + + "hmm. Why is there a text box above you ?"
                    - - - - - What Text Box ?.
                    + + + + ...
                    - - - - - -> TaxiDriverQuestions
    + + "So, how you doing ?"
        Look Pal, me and you just met, we're not there yet, are you going somewhere or not ?.
        + + + What a Mackerel!
            - - - - Whatever man!.
        + + + Don't have to be so rude!.
            - - - - Whatever man!.
        -> TaxiDriverQuestions
    + + "Nothing really!."
    - - ... ... What's wrong with you!.
    + + End
-> DONE

=== ConversationDaughi

= LoopConversation
Hey Jacki', yau got' anything new for me today ?.
    + Yeah, got some new shots her'. Will make a good piece.
     - - Then, head up to the office start writing.
     + + Go To Office.
        ~ OpenNewsPanel()
    -> DONE
    + Still looking  around, Daughi.
        - - Well, yau better pick up yau ass and git' workin'.
        + + Continue
        - - If we dun have anything by the end of the day. Both me and yau be hungry, git' it !?. 
        + + Ye ye yee, stop your yapping.
        + + Sure, Sure.
        - - Grrrr, git' going!.
        + + End
        -> DONE
    + So how does this publishing things work again ?, I kind of forgot!.
        - - ..... How do yau "forget" yau job ?!.
        + + Can't you just awnser me ?.
        + + Common my friend, Don't be a hardass.
        + + Just entertain me.
        - - Fineeee, Listen yau Mackerel!, yau go and take photos then come back to me to publish them. 
        -> DauglasQuestions
            -> DONE
    -> END.
= DauglasQuestions
    * Can't I just do that right after I take them pictures ?
        - - And how do yau intend to print the newspaper, out of yau ass ?!.
        -> DauglasQuestions
    * I remember being able to publish without going back to you.
        - - Did yau hit yau head or something ?
        -> DauglasQuestions
    * That seem like work, isn't ther' a faster way.
        - - Maybe because, i dun know, it IS WORK !?. Yau know how "work" works ?!.
        -> DauglasQuestions
    
    + ->
    * * Continue
    - - Yau know, people say that there is no such thing as a dumb question.
        + + + Please stop!.
        + + + Shut it!.
        + + + Whatever man!.
        - - - But I guess, there is an exception to everything.
            + + + + ....
            -> END
            + + + + Mackerel!.
            -> END
    -> DONE


-> END