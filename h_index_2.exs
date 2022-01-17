defmodule Solution do
  @spec h_index(citations :: [integer]) :: integer
  def h_index(citations) do
    max_h = min(Enum.max(citations), length(citations))
    try_h(citations, max_h)
  end

  def try_h(_citations, 0) do
    0
  end

  def try_h(citations, maybe_h) do
    Enum.reduce_while(citations, {0, 0, []}, fn c, {have_at_least_h, saves, rest} ->
      with {:qualify_for_h?, true} <- {:qualify_for_h?, c >= maybe_h},
           {:h_not_too_much?, true} <- {:h_not_too_much?, have_at_least_h + 1 <= maybe_h},
           new_saves <- if(c == maybe_h, do: saves + 1, else: saves) do
            {:cont, {have_at_least_h + 1, new_saves, rest}}
      else
        {:qualify_for_h?, false} -> {:cont, {have_at_least_h, saves, [c | rest]}}
        {:h_not_too_much?, false} ->
          # we can save an attempt by using one save, i.e. considering one "qualified" citation as "rest"
          cond do
            saves > 0 and c != maybe_h ->
              {:cont, {have_at_least_h, saves - 1, [maybe_h | rest]}}
            saves > 0 ->
              # if c == maybe_h it doesn't consume the save tho
              {:cont, {have_at_least_h, saves, [maybe_h | rest]}}
            true ->
              {:halt, :next}
          end
      end
    end)
    |> case do
      {^maybe_h, _, rest} ->
        if Enum.all?(rest, & &1 <= maybe_h), do: maybe_h, else: try_h(citations, maybe_h - 1)

      _ ->
        try_h(citations, maybe_h - 1)
    end
  end
end
