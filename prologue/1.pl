:- encoding(utf8).

digits(N, [N]) :-
    N < 10, !.
digits(N, List) :-
    N >= 10,
    Next is N // 10,
    Last is N mod 10,
    digits(Next, PartialList),
    append(PartialList, [Last], List).


main :-
    writeln('Введите натуральное число (или "exit" для выхода):'),
    read_line_to_string(user_input, Input),
    ( Input = "exit" ->
          writeln('Выход из программы.')
    ; number_string(N, Input) ->
          digits(N, List),
          format("Список цифр числа ~w: ~w~n~n", [N, List]),
          main
    ).