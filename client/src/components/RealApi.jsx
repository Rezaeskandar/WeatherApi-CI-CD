import React, { useState, useEffect } from 'react';
import axios from 'axios';
import styled from 'styled-components';

const api = {
    key: "55f8aa4b53f85e9514ba0c34e12a69b9",
    base: "http://api.openweathermap.org/data/2.5/weather?"
};

const WeatherApp = () => {
    const [city, setCity] = useState('');
    const [weather, setWeather] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axios.get(
                    `${api.base}q=${city}&appid=${api.key}&units=metric`
                );
                setWeather(response.data);
            } catch (error) {
                console.error(error);
            }
        };

        if (city !== '') {
            fetchData();
        }
    }, [city]);

    const handleSearch = () => {
        if (city !== '') {
            fetchData();
        }
    };

    const handleRestart = () => {
        setCity('');
        setWeather(null);
    };

    return (
        <Wrapper>
            <Title>Real Weather Search</Title>
            <SearchContainer>
                <SearchInput
                    type="text"
                    placeholder="Enter a city"
                    value={city}
                    onChange={(e) => setCity(e.target.value)}
                />
                <SearchButton onClick={handleSearch}>Search</SearchButton>
            </SearchContainer>
            {weather && (
                <WeatherContainer>
                    <Location>{weather.name}</Location>
                    <Temperature>Temperature: {weather.main.temp}°C</Temperature>
                    <WeatherCondition>{weather.weather[0].main}</WeatherCondition>
                </WeatherContainer>
            )}
            {weather && (
                <RestartButton onClick={handleRestart}>Restart</RestartButton>
            )}
        </Wrapper>
    );
};

const Wrapper = styled.div`
  text-align: center;
`;

const Title = styled.h1`
  margin-bottom: 1rem;
  padding-left: 3em;
`;

const SearchContainer = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  margin-bottom: 1rem;
  padding-left: 6em;
`;

const SearchInput = styled.input`
  padding: 0.5rem;
  margin-right: 0.5rem;
  border: none;
  border-radius: 4px;
  font-size: 16px;
  
`;

const SearchButton = styled.button`
  padding: 0.5rem 1rem;
  border: none;
  border-radius: 4px;
  font-size: 16px;
  background-color: #007bff;
  color: #fff;
  cursor: pointer;

  &:hover {
    background-color: #0056b3;
  }
`;

const WeatherContainer = styled.div`
  margin-top: 2rem;

`;

const Location = styled.h2`
  margin-bottom: 0.5rem;
`;

const Temperature = styled.p`
  font-size: 20px;
`;

const WeatherCondition = styled.p`
  font-size: 18px;
`;

const RestartButton = styled.button`
  padding: 0.5rem 1rem;
  border: none;
  border-radius: 4px;
  font-size: 16px;
  background-color: #dc3545;
  color: #fff;
  cursor: pointer;
  margin-top: 1rem;

  &:hover {
    background-color: #bd2130;
  }
`;

export default WeatherApp;
