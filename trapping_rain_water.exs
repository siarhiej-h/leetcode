defmodule Solution do
  @type left_border :: integer
  @type right_border :: integer
  @type in_between :: [integer]
  @type bordered_segment :: {left_border, in_between, right_border}

  @spec trap(height :: [integer]) :: integer
  def trap(height) do
    height
    |> to_bordered_segments()
    |> Enum.map(&trap_bordered/1)
    |> Enum.sum()
  end

  @spec to_bordered_segments(height :: [integer]) :: [bordered_segment]
  def to_bordered_segments([]), do: []
  def to_bordered_segments([_]), do: []
  def to_bordered_segments([_, _]), do: []

  def to_bordered_segments([first | _] = height) do
    [_, border | _] = Enum.sort(height, :desc)

    last = List.last(height)

    in_between = height |> Enum.drop(1) |> Enum.drop(-1)

    if first >= border && last >= border && Enum.all?(in_between, &(&1 < first && &1 < last)) do
      [{first, in_between, last}]
    else
      do_split(height, border)
    end
  end

  def do_split(height, border) do
    Enum.reduce(height, %{done: [], current: {nil, []}}, fn
      first_left_border, %{done: [], current: {nil, [_ | _] = current}}
      when first_left_border >= border ->
        done =
          [first_left_border | current]
          |> Enum.reverse()
          |> to_bordered_segments()

        %{done: done, current: {first_left_border, []}}

      before_first_border, %{done: [], current: {nil, [previous]}}
      when before_first_border >= previous ->
        %{done: [], current: {nil, [before_first_border]}}

      subsequent_border, %{done: done, current: {_, []}} when subsequent_border >= border ->
        %{done: done, current: {subsequent_border, []}}

      right_border, %{done: done, current: {left, in_between}} when right_border >= border ->
        %{
          done: [{left, Enum.reverse(in_between), right_border} | done],
          current: {right_border, []}
        }

      below_border, %{done: done, current: {left, in_between}} ->
        %{done: done, current: {left, [below_border | in_between]}}
    end)
    |> case do
      %{done: done, current: {_, []}} ->
        done

      %{done: done, current: {_, [_]}} ->
        done

      %{done: done, current: {left, in_between}} ->
        [left | Enum.reverse(in_between)]
        |> to_bordered_segments()
        |> Kernel.++(done)
    end
  end

  @spec trap_bordered(bordered_segment) :: integer
  def trap_bordered({left_border, in_between, right_border}) do
    min_border = min(left_border, right_border)

    in_between
    |> Enum.map(&(min_border - &1))
    |> Enum.sum()
  end
end
