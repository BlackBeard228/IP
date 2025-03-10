open System
open System.IO

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

let randomSeq () =
    let n = readNat "Введите количество элементов последовательности: "
    let low = -100.0
    let high = 100.0
    let rnd = System.Random()
    seq {
        for i in 1 .. n do
            let x = low + rnd.NextDouble() * (high - low)
            yield Math.Round(x, 2)
    }

let InputSeq () =
    let n = readNat "Введите количество элементов последовательности: "
    seq {
        for i in 1 .. n do
            let f = readFloat (sprintf "Введите элемент #%d: " i)
            yield f
    }

let readString prompt =
    printf "%s" prompt
    Console.ReadLine()

//Lab1
let getFirst n =
    let absNumber = abs n
    let s = absNumber.ToString()
    int (s.[0].ToString())

//Lab2
let count target lst =
    lst |> Seq.fold (fun acc x -> if x = target then acc + 1 else acc) 0

//Lab3
let rec getTxtFiles (dir: string) : seq<string> =
    seq {
        if Directory.Exists(dir) then
            // Получаем файлы с расширением ".txt" в текущем каталоге.
            for file in Directory.EnumerateFiles(dir, "*.txt") do
                yield file
            // Рекурсивно обрабатываем каждый подкаталог.
            for subDir in Directory.EnumerateDirectories(dir) do
                yield! getTxtFiles subDir
        else
            // Если каталог не существует, можно просто ничего не возвращать.
            ()
    }


//Menu
let rec chooseList () =
    printfn "Выберите способ ввода последовательности чисел:"
    printfn "1. Ввести последовательность вручную"
    printfn "2. Сгенерировать последовательность случайных чисел"
    let choice = readInt "Ваш выбор: "
    match choice with
    | 1 -> InputSeq ()
    | 2 -> randomSeq ()
    | _ ->
        printfn "Неверный выбор."
        chooseList ()

let rec mainMenu () =
    printfn "\nВыберите функцию:"
    printfn "1. Последовательность из цифр"
    printfn "2. Количество встреч числа в строке"
    printfn "3. Найти файлы txt в каталоге"
    printfn "0. Выход"
    let choice = readInt "Введите номер операции: "
    match choice with
    | 1 ->
        let sek = chooseList() |> Seq.cache
        printfn "Созданная последовательность чисел: %A" (Seq.toList sek)
        let newsek = sek |> Seq.map getFirst
        printfn "Последовательность из первых цифр чисел: %A" (Seq.toList newsek)
        mainMenu ()
    | 2 ->
        let sek = chooseList() |> Seq.cache
        printfn "Созданная последовательность чисел: %A" (Seq.toList sek)
        let x = readFloat "Введите число, которое нужно найти: "
        let res = count x sek
        printfn "Количество встеч числа %A: %A" x res
        mainMenu ()
    | 3 ->
        let directoryPath = readString "Введите путь к каталогу: "
        if Directory.Exists(directoryPath) then
            let txtFiles = getTxtFiles directoryPath
            printfn "\nНайденные файлы с расширением .txt:"
            txtFiles |> Seq.iter (printfn "%s")
        else
            printfn "\nУказанный каталог не существует: %s" directoryPath
        mainMenu()
    | 0 ->
        printfn "Выход из программы."
    | _ ->
        printfn "Неверный выбор. Попробуйте снова."
        mainMenu ()





let main =
    mainMenu()
    0

