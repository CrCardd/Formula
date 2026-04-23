# SceneMap
#### Construtor
    Para iniciar uma nova cena basta criar um objeto que herda de "SceneMap", esse objeto precisa ter um construtor que chame o método "Initialize()", que recebe o tamanho da janela e a profundidade(opcional, se for nulo não terá limites)

#### OnKeyDown - (override)
    Método para definir coisas para acontecerem quando uma tecla for pressionada

#### OnMouseDown - (override)
    Método para definir coisas para acontecerem quando algum dos botões do mouse for pressionado
#### OnMouseUp - (override)
    Método para definir coisas para acontecerem quando algum dos botões do mouse for solto
#### OnMouseMove - (override)
    Método para definir coisas para acontecerem quando o mouse estiver se movimentando

---
---
---
Dentro dos blocos do seu SceneMap também é possível usar qualquer método que aparece na seção a baixo "IWorld"
---
---
---
---
# IWorld

#### Depth - atributo  
    Profundidade do simulador (z, ou seja, quantos objetos cabem em uma única posição). 
    Default: null

#### Height - atributo
    Altura da tela da simulação em quadrados

#### Width
    Largura da tela da simulação em quadrados

#### MouseArgs
    Argumentos e propriedades do mouse da sua última posição


#### Destroy - método
    Parâmetros:
     - BaseObject obj
    Método para destruir um objeto. O objeto passado é aquele que será destruído.

#### SetFlag - método
    Parâmetros:
    - string key
    - bool value

    Retorno - void

    Para definir flags globais, por exemplo para definir um pause durante a simulação. 

#### GetFlag - método
    Parâmetros:
    - string key

    Retorno - bool

    Para capturar o valor de uma flag global.
    
#### GetGrid - método
    Parâmetros:
    - double x
    - double y
    - bool diagonal = false

    Retorno - Dictionary<Vector2D, IEnumerable<BaseObject>>
    Para pegar uma área ao redor de uma posição passada, incluindo o ponto passado. Retorna um dicionário, sendo a posição a chave, e uma lista com os objetos presentes nessa posição o valor

#### GetObjects - método
    Retorno - IReadOnlyCollection<BaseObject>

    Retorna uma lista com todos os objetos do mapa

#### GetPlace - método
    Parâmetros:
    - double x
    - double y

    Retorno - IEnumerable<BaseObject>

    Retorna uma lista como os objetos presentes nesta posição.
    
#### IsValid
    Parâmtros:
    - double x
    - double y

    Retorno - bool

    Indica se é uma posição válida ou não, se está dentro dos limites da tela e se cabe mais objetos naquela posição

#### NeighborObjects
    Parâmetros
    - double x
    - double y
    - bool diagonal = false

    Retorno - IEnumerable<BaseObject>
    Retorna os vizinhos direto de uma posição, podendo pegar os cantos ou não (controlado por 'diagonal')

#### New
    Parâmetros
    - BaseObject obj

    Retorno - BaseObject
    Cria um novo individuo

#### RadiusAreaObjects
    Parâmetros
    - double x
    - double y
    - int n

    Retorno - IEnumerable<BaseObject>

    Pega todos os objetos a partir de um ponto, sem considerar o ponto passado, podendo controlar o raio de objetos que irá pegar através de n.
    Por exemplo, se n = 1, irá retornar no máximo 8 objetos (3x3 - o objeto do meio)


# Behavior
#### Execute - (override)
    Parâmetros:
    - BaseObject obj
    - IWorld world
    Método que precisa ser definido para que a execução da Estratégia seja válida

#### OnMouseClick - (override)
#### OnMouseDown - (override)
#### OnMouseUp - (override)
#### OnMouseHover - (override)