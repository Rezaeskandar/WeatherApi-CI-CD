


import React, { useState } from 'react';
import axios from 'axios';
import styled from 'styled-components';

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

function StatisticsComponent() {
  const [apiCall, setApiCall] = useState(0);

  const fetchData = async () => {
    try {
      const response = await axios.get('https://dev.kjeld.io:40200/API/call/statistics');
      const data = response.data;
      // Extract the count from the response string
      //const count = parseInt(data.match(/\d+/)[0]);
      //const count = parseInt(data, 10) || 0;//parseInt(data, 10) - The parseInt function is used to parse the data string as an integer. The second argument 10 specifies the base (decimal) for parsing.
      const count = parseInt(data.split(':')[1].trim()) || 0;//splits the response string by ":" and retrieves the second part, which represents the count value. The trim() function is used to remove any leading or trailing 
      setApiCall(count);
    } catch (error) {
      console.error(error);
    }
  };

  const handleButtonClick = async () => {
    await fetchData();
  };

  return (
    <Background>
      <Container>
        <Statistics>We have Made {apiCall} API Calls</Statistics>
        <button onClick={handleButtonClick}>Get API Calls</button>
      </Container>
    </Background>
  );
}

export default StatisticsComponent;


