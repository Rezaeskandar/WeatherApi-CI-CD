import React, { useState, useEffect } from 'react';
import axios from 'axios';
import styled from 'styled-components' ;


const Background = styled.div`
  background: linear-gradient(135deg, #33ff00, #00ff9d);
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
`;

const Container = styled.div`
  background-color: rgba(248, 248, 248, 0.8);
  padding: 40px;
  border-radius: 12px;
  text-align: center;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
  backdrop-filter: blur(8px);
`;

const Statistics = styled.div`
  margin-bottom: 20px;
`;

function Statistices() {
  const [apiCall, setApiCall] = useState([]);
 
   useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get('https://dev.kjeld.io:40200/API/call/statistics');
        const data = response.data;
        setApiCall(data.apiCall);
      } catch (error) {
        console.error(error);
      }
    };

    fetchData();
  }, []);

  return(
    <>
    <Background>
        <Container>
            <Statistics>We have Called API {apiCall} </Statistics>
        </Container>
    </Background>
       
    </>
        
  )
}

export default Statistices;

