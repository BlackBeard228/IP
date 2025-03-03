// For more information see https://aka.ms/fsharp-console-apps
open System

let rec readInt prompt =
    printf "%s" prompt
    match System.Int32.TryParse(Console.ReadLine()) with
    | (true, value) -> value
    | _ ->
        printfn "Неверный ввод. Введите целое число."
        readInt prompt

let rec readFloat prompt =
    printf "%s" prompt
    match System.Double.TryParse(Console.ReadLine()) with
    | (true, value) -> value
    | _ ->
        printfn "Неверный ввод. Введите число."
        readFloat prompt

let rec readChar prompt =
    printf "%s" prompt
    let input = Console.ReadLine()
    if String.IsNullOrEmpty(input) || input.Length <> 1 then
        printfn "Неверный ввод. Введите один символ."
        readChar prompt
    else
        input.[0]

let rec readNat prompt =
    printf "%s" prompt
    match System.Int32.TryParse(Console.ReadLine()) with
    | (true, value) -> 
        if value <= 0 then
            printfn "Неверный ввод. Введите натуральное число."
            readNat prompt
        else 
            value
    | _ ->
        printfn "Неверный ввод. Введите натуральное число."
        readNat prompt

let randomList () =
    let n = readInt "Введите количество элементов списка: "
    let low = -100.0
    let high = 100.0
    let rnd = System.Random()
    [ for i in 1 .. n -> 
        let x = low + rnd.NextDouble() * (high - low) 
        Math.Round(x,2)
    ]

let InputList () =
    let n = readInt "Введите количество элементов списка: "
    let rec loop count acc =
        if count = 0 then List.rev acc
        else 
            let f = readFloat (sprintf "Введите элемент #%d: " (List.length acc + 1))
            loop (count - 1) (f :: acc)
    loop n []

//Lab1
let getFirst n =
    let absNumber = abs n
    let s = absNumber.ToString()
    int (s.[0].ToString())

//Lab2
let count target lst =
    lst |> List.fold (fun acc x -> if x = target then acc + 1 else acc) 0






//Menu
let rec chooseList () =
    printfn "Выберите способ ввода списка чисел:"
    printfn "1. Ввести список вручную"
    printfn "2. Сгенерировать список случайных чисел"
    let choice = readInt "Ваш выбор: "
    match choice with
    | 1 -> InputList ()
    | 2 -> randomList ()
    | _ ->
        printfn "Неверный выбор."
        chooseList ()

let rec mainMenu () =
    printfn "\nВыберите функцию:"
    printfn "1. Список из цифр"
    printfn "2. Количество встреч числа в строке"
    printfn "0. Выход"
    let choice = readInt "Введите номер операции: "
    match choice with
    | 1 ->
        let sp = chooseList()
        printfn "Созданный список чисел: %A" sp
        let newsp = List.map getFirst sp
        printfn "Список из первых цифр чисел: %A" newsp
        mainMenu ()
    | 2 ->
        let sp = chooseList()
        printfn "Созданный список чисел: %A" sp
        let x = readFloat "Введите число, которое нужно найти: "
        let res = count x sp
        printfn "Количество встеч числа %A: %A" x res
        mainMenu ()
    | 0 ->
        printfn "Выход из программы."
    | _ ->
        printfn "Неверный выбор. Попробуйте снова."
        mainMenu ()





let main =
    mainMenu()
    0
