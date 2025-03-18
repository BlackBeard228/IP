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

let randomString (rnd: System.Random) (length: int) =
    let chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789" |> Seq.toArray
    Array.init length (fun _ -> chars.[rnd.Next(chars.Length)]) |> System.String


type Tree<'T> =
    | Empty
    | Node of 'T * Tree<'T> * Tree<'T>


let rec mapTree (f: 'T -> 'U) (tree: Tree<'T>) : Tree<'U> =
    match tree with
    | Empty -> Empty
    | Node(value, left, right) -> Node(f value, mapTree f left, mapTree f right)

let rec foldTree (f: 'a -> 'T -> 'a) (acc: 'a) (tree: Tree<'T>) : 'a =
    match tree with
    | Empty -> acc
    | Node(value, left, right) ->
        let accLeft = foldTree f acc left
        let accWithCurrent = f accLeft value
        foldTree f accWithCurrent right

let rec printTree (toStr: 'T -> string) (indent: int) (tree: Tree<'T>) =
    let padding = String.replicate indent "  "
    match tree with
    | Empty ->
        printfn "%s- (Empty)" padding
    | Node(value, left, right) ->
        printfn "%s%s" padding (toStr value)
        printTree toStr (indent + 1) left
        printTree toStr (indent + 1) right


let rec createStrTreeManually () : Tree<string> =
    printf "Введите строку для узла (или пустую строку для отсутствия узла): "
    let input = Console.ReadLine()
    if String.IsNullOrWhiteSpace(input) then
        Empty
    else
        printf "Создать левое поддерево для узла '%s'? (Y/N): " input
        let left =
            if Console.ReadLine().Trim().ToUpper() = "Y" then createStrTreeManually () else Empty
        printf "Создать правое поддерево для узла '%s'? (Y/N): " input
        let right =
            if Console.ReadLine().Trim().ToUpper() = "Y" then createStrTreeManually () else Empty
        Node(input, left, right)


let createStrTreeRandomly () : Tree<string> =
    let depth = readInt "Введите максимальную глубину дерева: "
    let rnd = System.Random()
    let rec create currentDepth =
        if currentDepth <= 0 then Empty
        else
            let len = rnd.Next(1,5)
            let content = randomString rnd len
            let left = if rnd.NextDouble() > 0.5 then create (currentDepth - 1) else Empty
            let right = if rnd.NextDouble() > 0.5 then create (currentDepth - 1) else Empty
            Node(content, left, right)
    create depth


let rec chooseStrTreeCreation () =
    printfn "\nВыберите способ создания дерева строк:"
    printfn "1. Ввести дерево вручную"
    printfn "2. Сгенерировать дерево случайным образом"
    printf "Ваш выбор: "
    match Console.ReadLine().Trim() with
    | "1" -> createStrTreeManually ()
    | "2" -> createStrTreeRandomly ()
    | _ ->
        chooseStrTreeCreation ()


let rec createIntTreeManually () : Tree<int> =
    printf "Введите целое число для узла (или пустую строку для отсутствия узла): "
    let input = Console.ReadLine()
    if String.IsNullOrWhiteSpace(input) then
        Empty
    else
        match System.Int32.TryParse(input) with
        | (true, value) ->
            printf "Создать левое поддерево для узла '%d'? (Y/N): " value
            let left = if Console.ReadLine().Trim().ToUpper() = "Y" then createIntTreeManually () else Empty
            printf "Создать правое поддерево для узла '%d'? (Y/N): " value
            let right = if Console.ReadLine().Trim().ToUpper() = "Y" then createIntTreeManually () else Empty
            Node(value, left, right)
        | _ ->
            printfn "Неверный ввод. Попробуйте снова."
            createIntTreeManually ()


let createIntTreeRandomly () : Tree<int> =
    let depth = readInt "Введите максимальную глубину дерева: "
    let rnd = System.Random()
    let rec create currentDepth =
        if currentDepth <= 0 then Empty
        else
            let value = rnd.Next(100)
            let left = if rnd.NextDouble() > 0.5 then create (currentDepth - 1) else Empty
            let right = if rnd.NextDouble() > 0.5 then create (currentDepth - 1) else Empty
            Node(value, left, right)
    create depth

let rec chooseIntTreeCreation () =
    printfn "\nВыберите способ создания дерева целых чисел:"
    printfn "1. Ввести дерево вручную"
    printfn "2. Сгенерировать дерево случайным образом"
    printf "Ваш выбор: "
    match Console.ReadLine().Trim() with
    | "1" -> createIntTreeManually ()
    | "2" -> createIntTreeRandomly ()
    | _ ->
        printfn "\nНеверный выбор. Попробуйте снова."
        chooseIntTreeCreation ()


let rec mainMenu () =
    printfn "\nВыберите операцию:"
    printfn "1. Преобразовать дерево строк (map): добавить символ в конец каждой строки"
    printfn "2. Сформировать список из четных элементов дерева целых чисел (fold)"
    printfn "0. Выход"
    printf "Ваш выбор: "
    match Console.ReadLine().Trim() with
    | "1" ->
        let tree = chooseStrTreeCreation ()
        printfn "\nИсходное дерево строк:"
        printTree id 0 tree
        let symbol = (readChar "Введите символ, который нужно добавить к каждой строке: ").ToString()
        
        let transformedTree = mapTree (fun s -> s + symbol) tree
        printfn "\nПреобразованное дерево строк (с добавленным символом '%s'):" symbol
        printTree id 0 transformedTree
        mainMenu ()
    | "2" ->
        let tree = chooseIntTreeCreation ()
        printfn "\nИсходное дерево целых чисел:"
        printTree (fun x -> x.ToString()) 0 tree
        let evenList =
            let folded = foldTree (fun acc value -> if value % 2 = 0 then value :: acc else acc) [] tree
            List.rev folded
        printfn "\nСписок четных элементов дерева:"
        evenList |> List.iter (printfn "%d")
        mainMenu ()
    | "0" ->
        printfn "\nВыход из программы."
    | _ ->
        printfn "\nНеверный выбор. Попробуйте снова."
        mainMenu ()


let main =
    mainMenu ()
    0