:- encoding(utf8).


set_diff([], _, []).
set_diff([H|T], Set, [H|Diff]) :-
    \+ member(H, Set),
    set_diff(T, Set, Diff).
set_diff([H|T], Set, Diff) :-
    member(H, Set),
    set_diff(T, Set, Diff).


sym_diff(SetA, SetB, Diff) :-
    set_diff(SetA, SetB, Diff1),
    set_diff(SetB, SetA, Diff2),
    append(Diff1, Diff2, Diff).


main :-
    writeln('Введите первое множество (как список, например, [1,2,3]):'),
    read(Input1),
    writeln('Введите второе множество (как список, например, [2,3,4]):'),
    read(Input2),
    ( Input1 == exit ; Input2 == exit ->
          writeln('Выход из программы.')
    ; sym_diff(Input1, Input2, Diff),
      format("Симметричная разность между ~w и ~w равна: ~w~n~n", [Input1, Input2, Diff]),
      main
    ).