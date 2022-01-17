defmodule Solution do
  @spec shopping_offers(price :: [integer], special :: [[integer]], needs :: [integer]) :: integer
  def shopping_offers(price, special, needs) do
    consider_offers(0, price, special, needs)
  end

  def consider_offers(spent, price, special, needs) do
    special ++ [nil]
    |> Enum.map(fn offer -> apply_offer(spent, price, special, needs, offer) end)
    |> Enum.min()
  end

  def apply_offer(spent, price, _special, needs, nil) do
    spent + buy_regular(needs, price)
  end

  def apply_offer(spent, price, special, needs, chosen_offer) do
    case buy_offer(needs, chosen_offer) do
      {:error, :overbuy} -> spent + buy_regular(needs, price)
      {:ok, new_needs} -> consider_offers(spent + List.last(chosen_offer), price, special, new_needs)
    end
  end

  def buy_offer(needs, chosen_offer) do
    new_needs =
      needs
      |> Enum.zip(chosen_offer)
      |> Enum.map(fn {need, supply} -> need - supply end)

    if Enum.any?(new_needs, fn n -> n < 0 end) do
      {:error, :overbuy}
    else
      {:ok, new_needs}
    end
  end

  def buy_regular(needs, price) do
    needs
    |> Enum.zip(price)
    |> Enum.map(fn {need, price} -> need * price end)
    |> Enum.sum()
  end
end

[0,0,0]
[[1,1,0,4],[2,2,1,9]]
[2,2,1]
