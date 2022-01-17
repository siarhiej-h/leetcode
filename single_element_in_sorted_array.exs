defmodule Solution do
  @spec single_non_duplicate(nums :: [integer]) :: integer
  def single_non_duplicate(nums) do
    search(nums)
  end

  def search([a, a | tail]), do: search(tail)

  def search([a, _b | _tail]), do: a

  def search([a]), do: a
end
