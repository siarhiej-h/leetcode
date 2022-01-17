defmodule Solution do
  @spec reinitialize_permutation(n :: integer) :: integer
  def reinitialize_permutation(n) do
    perm = Enum.to_list(0..(n-1))
    permutate(perm, perm, n, 0)
  end

  def permutate(current, current, _n, iteration) when iteration > 0 do
    iteration
  end

  def permutate(current, target, n, iteration) do
    for {_, i} <- Enum.with_index(current) do
      if rem(i, 2) == 0 do
        Enum.at(current, ceil(i / 2))
      else
        Enum.at(current, ceil((n / 2) + ((i - 1) / 2)))
      end
    end
    |> permutate(target, n, iteration + 1)
  end
end
