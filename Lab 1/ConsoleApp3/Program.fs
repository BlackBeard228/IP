// For more information see https://aka.ms/fsharp-console-apps
open System

//Vvod
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

//Labs 1
let chered a1 a2 n =
    let sp = 
        [ for i in 0 .. (n - 1) do
                if i % 2 = 0 then 
                    yield a1 
                else 
                    yield a2]
    sp

//Labs 2
let rec sum n =
    if n = 0 then 0
    else n % 10 + sum (n / 10)

//Labs 3
type Complex = { Re : float; Im : float }

// Вывод комплексного числа в виде строки
let vivComplex c =
    if c.Im > 0.0 then sprintf "%A + %Ai" c.Re c.Im
    elif c.Im = 0.0 then sprintf "%A" c.Re
    else sprintf "%A - %Ai" c.Re (abs c.Im)

// (a+bi) + (c+di)
let addComplex c1 c2 = 
    { Re = c1.Re + c2.Re; Im = c1.Im + c2.Im }

// (a+bi) - (c+di)
let subtractComplex c1 c2 = 
    { Re = c1.Re - c2.Re; Im = c1.Im - c2.Im }

// (a+bi)*(c+di)
let multiplyComplex c1 c2 =
    { Re = c1.Re * c2.Re - c1.Im * c2.Im;
      Im = c1.Re * c2.Im + c1.Im * c2.Re }

// (a+bi)/(c+di)
let divideComplex c1 c2 =
    let del = c2.Re * c2.Re + c2.Im * c2.Im 
    if Math.Abs(del) < 1e-10 then failwith "Деление на 0 невозможно"
    { Re = (c1.Re * c2.Re + c1.Im * c2.Im) / del;
      Im = (c1.Im * c2.Re - c1.Re * c2.Im) / del }

// Возведение комплексного числа в целую степень, используя переход в полярную форму
let rec powComplex c n =
    if n = 0 then { Re = 1.0; Im = 0.0 }
    elif n < 0 then 
        // Для отрицательной степени сначала возводим в положительную, затем берём обратное число
        let posPower = powComplex c (-n)
        let del = posPower.Re * posPower.Re + posPower.Im * posPower.Im
        { Re = posPower.Re / del; Im = - posPower.Im / del }
    else
        // Переход в полярную форму: r = sqrt(a^2+b^2), phi = atan2(b, a)
        let r = sqrt(c.Re * c.Re + c.Im * c.Im)
        let phi = atan2 c.Im c.Re
        let rPow = pown r n
        // Применяем теорему Муавра: (r*e^(iphi))^n = r^n * e^(i*n*phi)
        { Re = rPow * cos (float n * phi); Im = rPow * sin (float n * phi) }

let powComplexReal c (a: float) =
    let r = sqrt(c.Re * c.Re + c.Im * c.Im)
    let phi = atan2 c.Im c.Re
    let rPow = Math.Pow(r, a)
    { Re = rPow * cos (a * phi); Im = rPow * sin (a * phi) }

// Vvod Complex
let readComplex prompt =
    printfn "\n%s" prompt
    let realPart = readFloat "Введите вещественную часть: "
    let imagPart = readFloat "Введите мнимую часть: "
    { Re = realPart; Im = imagPart }

// Menu Complex
let rec complexOperationsMenu () =
    printfn "\nВыберите операцию над комплексными числами:"
    printfn "1. Сложение"
    printfn "2. Вычитание"
    printfn "3. Умножение"
    printfn "4. Деление"
    printfn "5. Возведение в степень"
    printfn "6. Возведение в вещественную степень"
    printfn "0. Назад в главное меню"
    let choice = readInt "Введите номер операции: "
    match choice with
    | 1 ->
        let c1 = readComplex "Введите первое комплексное число:" 
        let c2 = readComplex "Введите второе комплексное число:" 
        let res = addComplex c1 c2
        printfn "Сумма: %s" (vivComplex res)
        complexOperationsMenu ()
    | 2 ->
        let c1 = readComplex "Введите первое комплексное число:" 
        let c2 = readComplex "Введите второе комплексное число:" 
        let res = subtractComplex c1 c2
        printfn "Разность: %s" (vivComplex res)
        complexOperationsMenu ()
    | 3 ->
        let c1 = readComplex "Введите первое комплексное число:" 
        let c2 = readComplex "Введите второе комплексное число:" 
        let res = multiplyComplex c1 c2
        printfn "Произведение: %s" (vivComplex res)
        complexOperationsMenu ()
    | 4 ->
        let c1 = readComplex "Введите первое комплексное число:" 
        let c2 = readComplex "Введите второе комплексное число:" 
        try
            let res = divideComplex c1 c2
            printfn "Частное: %s" (vivComplex res)
        with ex ->
            printfn "Ошибка: %s" ex.Message
        complexOperationsMenu ()
    | 5 ->
        let c = readComplex "Введите комплексное число:" 
        let n = readInt "Введите целую степень: "
        let res = powComplex c n
        printfn "Результат возведения в степень: %s" (vivComplex res)
        complexOperationsMenu ()
    | 6 ->
        let c = readComplex "Введите комплексное число: " 
        let n = readFloat "Введите вещественную степень: "
        let res = powComplexReal c n
        printfn "Результат возведения в нецелую степень: %s" (vivComplex res)
        complexOperationsMenu ()
    | 0 -> () // Возврат в главное меню
    | _ ->
        printfn "Неверный выбор. Попробуйте снова."
        complexOperationsMenu ()

// Menu
let rec mainMenu () =
    printfn "\nВыберите функцию:"
    printfn "1. Список из двух чередующихся символов"
    printfn "2. Сумма цифр натурального числа"
    printfn "3. Операции над комплексными числами"
    printfn "0. Выход"
    let choice = readInt "Введите номер операции: "
    match choice with
    | 1 ->
        let a1 = readChar "Введите первый символ: "
        let a2 = readChar "Введите второй символ: "
        let n = readNat "Введите длину списка: "
        let sp = chered a1 a2 n
        printfn "Список чередующихся символов: %A" sp
        mainMenu ()
    | 2 ->
        let x = readNat "Введите натуральное число: "
        let summ = sum x
        printfn "Сумма цифр числа %A равна %A" x summ
        mainMenu ()
    | 3 ->
        complexOperationsMenu ()
        mainMenu ()
    | 0 ->
        printfn "Выход из программы."
    | _ ->
        printfn "Неверный выбор. Попробуйте снова."
        mainMenu ()

// Main
let main = 
    mainMenu ()
    0
    