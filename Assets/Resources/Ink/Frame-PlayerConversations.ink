EXTERNAL OpenTaxiMap()
EXTERNAL OpenNewsPanel()
VAR JustPublishedPaper = false
VAR newPaperCommiePoint = 0
VAR newPaperCapitalPoint = 0

-> Conversations.TaxiDriver
=== Conversations ===
= TaxiDriverStartsConversation
Hey!
    + Hey!
    -> TaxiDriver
= TaxiDriver 
Ya' want to go some where!?
    + Yup, Let me sei' the map.
    ~ OpenTaxiMap()
    -> END
    + No, just passing by.
    + I don't know yet, I have some questions.
    And I ain't have no anwsers!
        + + Don't be an asshole now!.
            -> TaxiDriverQuestions
        + + Just some questions her'.
            -> TaxiDriverQuestions
            
        + + You know what, never mind, I'll bei' on my way.
    - See ya' around then.
    + See you around!.
    -> DONE
= TaxiDriverQuestions        
Hmm, sure, but ya' be quick with it!.
-> TaxiDriverQuestionsHub
= TaxiDriverQuestionsHub
    +  Where can you take me ?
        Because current emergency, I can only take ya' to Marestrom and Picis street and vice versa.
        + +  What's emergency ?
            Ya're not from here or something ?.
            -> AboutTheEmergency
    +  So, how you doing ?
        Look Pal, me and ya' just met, we're not there yet, are ya' going somewhere or not ?.
        + +  What an asshole!
        + +  Don't hav' to be so rude!.
        - - Whatever man!.
            -> TaxiDriverQuestions
    +  Nothing really!.
    -  ... ... What's wrong with ya'!.
    +  Bye!
-> DONE
= AboutTheEmergency
    + Yeah, sure!
    + I hav' not been out much.
    - Well, them elites have placed a restriction order on the whole countries!. 
        + +  What do you know about the emergency?.
        + +  Can you tell me more?.
        - - Man, ya don't read the news much huh? Wad Street and the Low Tide Dock are completele banned. No In and Out.
            + + + I sei', You know why ?
                Hell if I know, ya' go and ask them ya'self.
                
            + + + Did they say why?
                No, they didn't care or they didn't want to tell, either way, nothing we can do about it!.
            - - - -> TaxiDriverQuestionsHub
        
-> DONE
=== ConversationPoliceBlockStatueEvent

= StartConversation
I'm watching you!.
    + Hey ?.
    -> LoopConversation

= LoopConversation
Where do you think you're going ?. This area is blocked to all citizens.
    + I'm a journalist. I need to get in to report.
    + What's going on in there ?.

    - -> DONE
=== ConversationDaughi
= StartConversation
Jackie'!
    + Hey Daugie'!
    -> LoopConversation
= LoopConversation
{ 
    
    - JustPublishedPaper == true:
    
    { 
        - newPaperCommiePoint > newPaperCapitalPoint:
        { shuffle:
                - Going against the system ha. Be careful Jackie', the coppers might bite back.
                - Those coppers have been pretty bold these days. Good things that you are there to catch them in the act.
                - Some coppers have been walking by lately, with their eyes glaring at the shop.
                
        }
        - newPaperCommiePoint < newPaperCapitalPoint:
        
        { shuffle:
                - Look at those coppers being helpless against those renegades. Letting them causing so much chaos.
                - Somebody needs to put a stop to those renegades. Without the Law to regulate them, who knows what else they might do.
                - I feel for them Jackie', I do. They struggle and struggle for a better Maresland but this is just pointless violence.
        }
        - else:
        { shuffle:
                - What is this newspaper ?. There's no news in it, Jackie'. Nobody gonna buy this!!.
                - I hired yau' to find news, not to take scenary shots, Jackie'. We're going to run out of business like this.
                - I am sure that there are more interesting things out there to write about. This is just boring!.
        }
    }
    - else:
        Yau' got anything new for me today ?.
}
~ JustPublishedPaper = false
    + Got some new shots her'. Will make a good piece!.
     Then, head up to the office start writing.
     + + Sure.
        ~ OpenNewsPanel()
    -> DONE
    + Still looking around, Daugie'.
        Grrrr, git' going!.
        + + See you around Daugie'!.
        -> DONE
    + So how does this publishing things work again ?, I kind of forgot!.
        ..... How do yau' "forgot" yau' job ?!.
        + + Can't you just awnser me ?.
        + + Common my friend, Don't be an asshole!.
        + + Just entertain me.
        - - Fineeee, Listen yau' shit head!, yau go and take photos then come back to me to publish them. 
        -> DauglasQuestions
    + Daugie', I want some advice.
        - - Grrr, on what ?
        -> DauglasAdvices

    -> END.
= DauglasQuestions
    * Can't I just do that right after I take them pictures ?
        - - And how do yau' intend to print the newspaper, out of yau' ass ?!.
        -> DauglasQuestions
    * I remember being able to publish without going back to you.
        - - Did yau' hit yau' head or something ?
        -> DauglasQuestions
    * That seem like work, isn't there a faster way.
        - - Maybe because, i dun know, it IS WORK !?. Yau know how "work" works ?!.
        -> DauglasQuestions
    + That's all.
        - - Yau know, people say that there is no such thing as a dumb question.
            + + Please stop!.
            + + Shut it!.
            + + Whatever man!.
            - - But I guess, there is an exception to everything.
                + + + ....
                + + + Fuck!.
                - - - -> END
= DauglasAdvices
    * You know where things might get interesting ?
        - - ... isn't that yau' job to find out ? Yau' know, the one I'm paying yau' for.
            + + + Well, any tips would help!.
            + + + Just tell me.
            + + + Why were you written like this ?
            - - - ... how about yau' go around and, I dun know, look?!
                + + + + Fuck!
                - - - - but besides from that, grrr, listen to gossips, know what people are talking!.
                    + + + + + Thanks Daugie'.
                    + + + + + Good Daugie'.
                    + + + + + That's not so hard now, isn't it!?.
                    - - - - - ....
                    -> DauglasAdvices
    * What kind of photos are we looking for ?
        Look around yau', we are about' to start another civil war.
        + + I can see that!.
        + + It is that bad, huh ?!.
        - - People around here wants news, Jackie'. They want to know which side to pick.
        ->  DauglasAdvices
    + That's all, Dougie'
    -> END
        