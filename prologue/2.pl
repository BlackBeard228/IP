:- encoding(utf8).


min_list([H], H).
min_list([H|T], Min) :-
    min_list(T, TailMin),
    (H =< TailMin -> Min = H ; Min = TailMin).


count_occurrences([], _, 0).
count_occurrences([H|T], Element, Count) :-
    count_occurrences(T, Element, CountTail),
    (H =:= Element -> Count is CountTail + 1 ; Count = CountTail).


minimal_occurrences(List, Count) :-
    min_list(List, Min),
    count_occurrences(List, Min, Count).


main :-
    writeln('Введите список чисел (например, [3,1,4,1,5]).'),
    writeln('Для выхода введите exit.'),
    read(Input),
    ( Input == exit ->
          writeln('Выход из программы.')
    ; is_list(Input) ->
          minimal_occurrences(Input, Count),
          min_list(Input, Min),
          format("Минимальный элемент ~w встречается ~w раз(а) в списке ~w~n~n", [Min, Count, Input]),
          main
    ).