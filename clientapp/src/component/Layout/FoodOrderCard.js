import React from "react";

function FoodOrderCard(props) {
  return (
    <tr>
      <td key={props.FoodItems.orderId}>{props.FoodItems.orderId}</td>
      <td>
        {props.FoodItems.foodItemList.map((food) => (
          <tr>{food.foodName}</tr>
        ))}
      </td>
      <td>
        {props.FoodItems.foodItemList.map((food) => (
          <tr>x{food.quantity}</tr>
        ))}
      </td>
    </tr>
  );
}

export default FoodOrderCard;
