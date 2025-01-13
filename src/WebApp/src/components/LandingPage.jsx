import React from 'react';
import { Link } from 'react-router-dom';

function LandingPage() {
    return (
      <div style={{ textAlign: 'center', padding: '20px' }}>
        <h1>Welcome to the Todo List App</h1>
        <Link to="/todos">
          <button>Go to Todo List</button>
        </Link>
      </div>
    );
  }
  
  export default LandingPage;