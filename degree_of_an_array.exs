defmodule Solution do
  @spec find_shortest_sub_array(nums :: [integer]) :: integer
  def find_shortest_sub_array(nums) do
    {elements, frequency} =
      Enum.frequencies(nums)
      |> Enum.reduce({[], 0}, fn {el, f}, {current, best_f} ->
        cond do
          f == best_f ->
            {[el | current], best_f}

          f > best_f ->
            {[el], f}

          f < best_f ->
            {current, best_f}
        end
      end)

    elements
    |> Enum.map(& find_first_to_last_length(nums, &1))
    |> Enum.min()
  end

  def find_first_to_last_length(nums, el) do
    nums
    |> Enum.drop_while(& &1 != el)
    |> Enum.reverse()
    |> Enum.drop_while(& &1 != el)
    |> length()
  end
end
