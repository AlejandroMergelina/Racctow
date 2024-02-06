INCLUDE globals.ink
hola1#speaker:Paco

{actitud == "": ->main | ->alreadyChose}



=== main ===
Hola rrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr#speaker:Paco
¿Comovas?
    +[bien]
        Bien verdad #speaker:Prota
        -->EJEMPLO("bien")
    +[mal]
        Como que mal #speaker:Prota
        -->EJEMPLO("mal")

->DONE



=== EJEMPLO(chosen) ===
~ actitud = chosen
bueno en relidad me da igual, pero sabias que <color=\#FF1E35> Tom Holand </color> va a ser el nuevo <color=\#FF1E35> Dani Fantom </color>
entonces adios
-> END

=== alreadyChose ===
ya he hablado contigo, largate
->END
