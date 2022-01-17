defmodule Solution do
  @spec count_balls(low_limit :: integer, high_limit :: integer) :: integer
  def count_balls(low_limit, high_limit) do
    for ball <- low_limit..high_limit do
      ball
      |> Integer.to_string()
      |> String.codepoints()
      |> Enum.map(&String.to_integer/1)
      |> Enum.sum()
    end
    |> Enum.frequencies()
    |> Map.values()
    |> Enum.max()
  end
end
