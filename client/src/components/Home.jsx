

import React, { useState, useEffect } from 'react';
import axios from 'axios';
import styled from 'styled-components';

const Button = styled.button`
  font-size: 16px;
  padding: 10px 20px;
  border-radius: 8px;
  background-color: #fff;
  color: #0072ff;
  border: 1px solid #0072ff;
  cursor: pointer;
  outline: none;
`;

const Background = styled.div`
  background: linear-gradient(135deg, #00c6ff, #0072ff);
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

const WeatherInfo = styled.div`
  margin-bottom: 20px`; 

 const Temperature = styled.div`
   font-size: 48px;
   font-weight: bold;
  color: hsl(207, 75%, 43%);
`;

 const WeatherProperty = styled.div`
   font-size: 24px;
   color: hsl(0, 0%, 40%);
 `;




const StockholmWeather = () => {
  const [weatherData, setWeatherData] = useState(null);
  const [showWeather, setShowWeather] = useState(false);

  const fetchData = async () => {
    try {
      const response = await axios.get('https://dev.kjeld.io:40200/weatherforecast-Stockholm'); 
      const data = response.data;
      setWeatherData(data.weatherData);
      setShowWeather(true);
    } catch (error) {
      console.error(error);
    }
  };

  const handleClose = () => {
    setShowWeather(false);
  };

  return (
    <Background>
      <Container>
        {showWeather ? (
          <>
            <WeatherInfo>
              <Temperature>{weatherData.temperature.value} {weatherData.temperature.unit}</Temperature>
              <WeatherProperty>{weatherData.location}</WeatherProperty>
            </WeatherInfo>
            <WeatherProperty>
              Humidity: {weatherData.humidity.value} {weatherData.humidity.unit}
            </WeatherProperty>
            <WeatherProperty>
              Wind: {weatherData.wind.value} {weatherData.wind.unit}
            </WeatherProperty>
            <Button onClick={handleClose}>Close</Button>
          </>
        ) : (
          <Button onClick={fetchData}>Get Stockholm Weather</Button>
        )}
      </Container>
    </Background>
  );
};

export default StockholmWeather;



