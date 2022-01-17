defmodule Solution do
  @spec people_indexes(favorite_companies :: [[String.t]]) :: [integer]
  def people_indexes(favorite_companies) do
    companies = Enum.map(favorite_companies, &MapSet.new/1)

    for {person, i} <- Enum.with_index(companies) do
      Enum.reduce_while(companies, i, fn
        ^person, acc -> {:cont, acc}
        p, acc ->
          if MapSet.subset?(person, p) do
            {:halt, :no}
          else
            {:cont, acc}
          end
      end)
    end
    |> Enum.reject(& &1 == :no)
    |> Enum.sort()
  end
end
