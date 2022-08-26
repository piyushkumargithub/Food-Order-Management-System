import React, { useState } from "react";
import { Button, Form, Table } from "react-bootstrap";
import HeaderNav from "./HeaderNav";
import UnauthorizedAccess from "./UnauthorizedAccess";
let foodList = [
  "Samosa",
  "Pani Puri",
  "Pizza",
  "Chole Bhature",
  "Sandwich",
];
let quantityList = [0, 0, 0, 0, 0];
function FoodOrderCart() {
  
  
  
  const [FoodItems, setFoodItems] = useState();
  const [TotalValue, setTotalValue] = useState();
  const [DefaultValue,setDefaultValue] =useState(0);
  

  async function placeOrder(){
    console.log(quantityList);
    let inp=[]
    quantityList.forEach((element,index) => {
      if(element!==0){
        inp.push({foodName:foodList[index],
        quantity:parseInt(element)})  
      } 
      });
    let dataTobesent={
      userId:parseInt(localStorage.getItem("userid")),
      foodItemList:inp
    }
    console.log(JSON.stringify(dataTobesent));
    const url = `https://localhost:44306/api/Cart/`;
    let result = await fetch(url, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        Authorization: 'Bearer '+localStorage.getItem("token").replaceAll('"','')
      },
      body: JSON.stringify(dataTobesent),
    });
    result= await result.json();
    console.log(result);
    quantityList = [0, 0, 0, 0, 0];
    setDefaultValue(0);
    alert(result);
  }

  async function calculateTotal(){
    let inp=[]
    quantityList.forEach((element,index) => {
      if(element!==0){
        inp.push({foodName:foodList[index],
        quantity:parseInt(element)})  
      } 
      });
    
    let dataTobesent={
      userId:parseInt(localStorage.getItem("userid")),
      foodItemList:inp
    }
    console.log(JSON.stringify(dataTobesent));
    const url = `https://localhost:44306/api/Cart/TotalValue`;
    let result = await fetch(url, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        Authorization: 'Bearer '+localStorage.getItem("token").replaceAll('"','')
      },
      body: JSON.stringify(dataTobesent),
    });
    result= await result.json();
    console.log(result.value);
    setTotalValue(result.value);
  }

  return localStorage.getItem("token")?(
    <>
    <HeaderNav />
  <div className="col-lg-8 offset-lg-2 mt-5">
    <Table striped bordered hover>
      <thead>
        <tr>
        <th className="col-6">Food Item</th>
        <th className="col-2">Quantity</th>
        </tr>
      </thead>
      <tbody>
        {foodList.map((food, index) => (
          <tr>
            <td> {food} </td>
            <td>
              <Form.Control
                type="number"
                min={0}
                max={50}
                defaultValue={0}
                onChange={(e) => {
                  quantityList[index] = e.target.value;
                  
                }}
              ></Form.Control>
            </td>
          </tr>
        ))}
      </tbody>
    </Table>
    <Button style={{padding:"10px",margin:"25px"}} onClick={calculateTotal}>
      Calculate Cost
    </Button>

    Total Value to paid:
    <span className="totalprice">{TotalValue}</span>

    <Button style={{padding:"10px",margin:"25px",float: "right"}} onClick={placeOrder}>
      Place Order
    </Button>
    </div>
   </> 
  ) :(
    <UnauthorizedAccess/>
  );
}

export default FoodOrderCart;
