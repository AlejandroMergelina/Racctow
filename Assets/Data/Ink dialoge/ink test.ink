INCLUDE globals.ink
EXTERNAL PickUpItem()

{actitud == "": ->main | ->alreadyChose}



=== main ===
Hola#speaker:Paco
¿Comovas?
    +[bien]
        Bien verdad #speaker:Prota
        ->EJEMPLO("bien")
    +[mal]
        Como que mal #speaker:Prota
        ->EJEMPLO("mal")

->DONE



=== EJEMPLO(chosen) ===
~ actitud = chosen
Bueno en relidad me da igual, toma <color=\#FF1E35> esto </color> para que me dejes empaz.
~ PickUpItem()
-> END

=== alreadyChose ===
ya he hablado contigo, largate
->END
