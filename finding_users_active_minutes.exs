defmodule Solution do
  @spec finding_users_active_minutes(logs :: [[integer]], k :: integer) :: [integer]
  def finding_users_active_minutes(logs, k) do
    uams =
      logs
      |> uam_k_or_less(k, %{}, [])
      |> Map.values()
      |> Enum.map(& MapSet.size(&1))
      |> Enum.frequencies()

    for j <- 1..k do
      uams[j] || 0
    end
  end

  def uam_k_or_less([], _, acc, _stoplist) do
    acc
  end

  def uam_k_or_less([[id, minute] | rest], k, acc, stoplist) do
    if id in stoplist do
      uam_k_or_less(rest, k, acc, stoplist)
    else
      new_acc = Map.update(acc, id, MapSet.new([minute]), fn unique_user_minutes ->
        MapSet.put(unique_user_minutes, minute)
       end)

      if MapSet.size(new_acc[id]) > k do
        uam_k_or_less(rest, k, Map.delete(acc, id), [id | stoplist])
      else
        uam_k_or_less(rest, k, new_acc, stoplist)
      end
    end
  end
end
