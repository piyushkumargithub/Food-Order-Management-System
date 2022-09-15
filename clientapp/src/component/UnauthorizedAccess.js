import React from 'react';
import {  Link } from "react-router-dom";

function UnauthorizedAccess() {
  return (
    <div className="text-center">Unauthorized access to page
        <h1><p>Go to Login page from here</p>
        <Link to="/">Login</Link>
        </h1>
    </div>
  )
}

export default UnauthorizedAccess