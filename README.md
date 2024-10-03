# Лабораторні роботи з дисципліни "Кросплатформне програмування"

1. [Lab1](https://github.com/VitaliiHalayko/cross_platform_programming/tree/master/lab1)
1. [Lab2](https://github.com/VitaliiHalayko/cross_platform_programming/tree/master/lab2)
1. [Lab3](https://github.com/VitaliiHalayko/cross_platform_programming/tree/master/lab3)

## Виконав

студент 3 курсу групи ІПЗ-34мс Галайко Віталій

## Installation

```bash
  git clone https://github.com/VitaliiHalayko/cross_platform_programming
```

## Run Locally labN (N - номер лр)

Дана інформація буде продубльована для кожної лабораторної роботи з вказанням точного шляху до файлів для запуску та тестування (N - номер лр):

Зібрати застосунок:

```bash
  dotnet msbuild build.proj /p:Solution=labN /t:Build
```

Запустити застосунок:

```bash
  dotnet msbuild build.proj /p:Solution=labN /t:Run
```

## Test labN (N - номер лр)

Дана інформація буде продубльована для кожної лабораторної роботи з вказанням точного шляху до файлів для запуску та тестування (N - номер лр):

```bash
  dotnet msbuild build.proj /p:Solution=labN /t:Test
```

### Наприклад, до лабораторної роботи 1:

```bash
  dotnet msbuild build.proj /p:Solution=lab1 /t:Build
  dotnet msbuild build.proj /p:Solution=lab1 /t:Run
  dotnet msbuild build.proj /p:Solution=lab1 /t:Test
```
