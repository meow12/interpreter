﻿namespace interpreter
 
open System
open Microsoft.FSharp.Text.Lexing

module Entry = 
    open System.IO
    let path = "..\\..\\program-logic\\Program.xd"

    [<EntryPoint>]
    let main argv =
        let parse json = 
            let lexbuf = LexBuffer<char>.FromString json
            let res = try
                        Parser.start Lexer.tokens lexbuf
                      with e ->
                        let pos = lexbuf.EndPos
                        let line = pos.Line
                        let column = pos.Column
                        let message = e.Message
                        let lastToken = new System.String(lexbuf.Lexeme)
                        printfn "Parse failed at line %d, column %d" line column
                        printfn "Last loken: %s" lastToken
                        printfn "Press any key to continue..."
                        System.Console.ReadLine() |> ignore
                        exit 1
            res
        
        let code = 
            if (File.Exists path) then File.ReadAllText path
            else ""

        
        let parseResult = code |> parse
        printfn "%A" parseResult

        printfn "Press any key to continue..."
        System.Console.ReadLine() |> ignore
        0

    
