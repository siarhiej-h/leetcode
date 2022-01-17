defmodule Solution do
  @spec max_sub_array(nums :: [integer]) :: integer
  def max_sub_array(nums) do
    all_negative? =
      nums
      |> Enum.map(& &1 <=0)
      |> Enum.all?()

    cond do
      all_negative? -> Enum.max(nums)
      true -> do_max_sub_array(nums)
    end
  end

  def do_max_sub_array(nums) do
    do_max_sub_array(nums, 0, 0)
  end

  def do_max_sub_array([], _, best) do
    best
  end

  def do_max_sub_array([head | tail], current_sum, all_time_best) do
    new_sum = current_sum + head
    if new_sum > 0 do
      do_max_sub_array(tail, new_sum, max(new_sum, all_time_best))
    else
      do_max_sub_array(tail, 0, max(head, all_time_best))
    end
  end
end
