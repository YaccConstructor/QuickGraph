# Визуализация в QuickGraph

## Плагины

### Немного общих слов

Каждая визуализация — плагин, который грузится при старте MainForm. Плагины грузятся с помощью библиотеки `Mono.Addins`.

Каждый плагин должен реализовать некий заданный интерфейс.
Наш интерфейс лежит в файле [`IAlgorithm.fs`](https://github.com/YaccConstructor/QuickGraph/blob/GraphTasks/src/GraphTask.Common/IAlgorithm.fs), а пример класса, реализующего его, — в [`PluginSample.cs`](https://github.com/YaccConstructor/QuickGraph/blob/GraphTasks/src/GraphTask.PluginSample/PluginSample.cs), с ним стоит тщательно ознакомиться.

### Создаём плагин

Нужно завести новый проект, например `C# Class Library`, добавить ссылку на GraphTask.Common и создать в нём класс, реализующий интерфейс `IAlgorithm`.

Mono.Addins — внешняя зависимость, нужно добавить ссылку на сборку.
В QuickGraph используется `Paket` для установки внешних зависимостей и создания ссылок, он заменяет собой `NuGet` и умеет немного больше последнего.
Работать с ним нужно через командную строку.

Чтобы добавить ссылку на `Mono.Addins`, введите следующее, находясь в директории `QuickGraph`:

```
paket\paket.exe add nuget Mono.Addins project YourProjectNameHere
```

Чтобы `Mono.Addins` увидел плагин, нужно добавить немного аннотаций в вашем плагине:

```csharp
[assembly: Addin]
[assembly: AddinDependency("GraphTasks", "1.0")]

namespace PluginSample
{
    [Extension]
    public class PluginSample : IAlgorithm
    {
        ...
    }
}
```

`Mono.Addins` ищет плагины в определённых директориях. Мы кладём плагины `GraphTaskBin` в корне `QuickGraph`. Чтобы плагин оказался там, добавьте ссылку на плагин из `GraphTask.MainForm`, это заставит его компилироваться вместе с формочкой и после сборки отправляться в ту же директорию.

Проверяйте сборку через `build.cmd`, он вызывается на сервере. Для работы некоторых тестов нужно установить `Pex`, установщик лежит в `lib/Pex`.

## GraphX

`GraphX` — библиотека для рисования графов. В отличие от `QuickGraph`, где вершины могут иметь произвольный тип, в `GraphX` все вершины и рёбра должны наследоваться от классов `VertexBase` или `EdgeBase`, а рисовать можно только ориентированные графы. Для простоты использования был добавлен проект [`QuickGraph.GraphXAdapter`](https://github.com/YaccConstructor/QuickGraph/tree/GraphTasks/src/QuickGraph.GraphXAdapter), в котором отнаследованы классы вершин и рёбер, аккуратно скопированы неплохие настройки для визуализации. Работа с библиотекой наглядно показана в классе `PluginSample`.

## DOT

В доте есть ключевое слово `graph` для неориентированных графов и `digraph` — для ориентированных.
Если в доте задан неориентированный граф, а в `QuickGraph` парсер вызван из класса ориентированного графа, будет создано по ребру в каждую сторону.

У вершин и рёбер могут быть произвольные атрибуты, которыми можно задать вес, цвет, что угодно ещё.

```
graph {
  v       // вершина
  a -- b  // ребро
  
  c -- d [weight = 10]  // атрибут
  
  o -- m -- n  // можно задать сразу несколько рёбер

  node [a=b]  // начиная отсюда у всех вершин будет атрибут a со значением b
  edge [weight=7]
  
  { f g h } -- { x y z }  // два подграфа, соединены все их вершины, двудольный граф
}
```

```
digraph {
  a -> b
  f -> g -> h [color = Blue]
}
```

В адаптер для парсера нужно передать функции для создания вершин и рёбер определённых типов. Эти же функции могут обрабать атрибуты.
В [VertexFactory](https://github.com/YaccConstructor/QuickGraph/blob/GraphTasks/src/QuickGraph.GraphXAdapter/VertexFactory.cs) и [EdgeFactory](https://github.com/YaccConstructor/QuickGraph/blob/GraphTasks/src/QuickGraph.GraphXAdapter/EdgeFactory.cs) есть примеры функций, создающие вершины и рёбра для `GraphX`, оставляющие имена вершин, веса или все атрибуты. Можно написать свои для других атрибутов по аналогии.

## Решение проблем

### XamlParseException при старте

Проблема: сабмодуль GraphX подтянул старый коммит, нужно его обновить.

Решение: (в командной строке)

```
cd src/GraphX # это сабмодуль, проблема с ним
git fetch origin # берём коммит с фиксом
git reset —hard origin/PCL # применяем
``` 

Потом в студии `Build` – `Clean Solution`.

### При очередном старте формы пропали плагины

Проблема: `Mono.Addins` кеширует плагины, что-то разъехалось.

Решение: удалить директорию `mono.addins` в `users/YourUsernameHere/appdata/roaming`.

### Странные ошибки с символами в C# при сборке через build.cmd 

Возможная проблема: используются возможности C# 6.0, которые не понимает компилятор предыдущей версии.

Решение: добавить ссылку на сборку с новым компилятором:

```
paket\paket.exe add nuget Microsoft.Net.Compilers project YourProjectNameHere
```
