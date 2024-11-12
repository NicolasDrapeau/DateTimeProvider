# Présentation
`DateTimeProvider` est un concept qui n'est pas un type intégré dans la BCL. 
Ce concept représente une bonne pratique et un modèle de conception qu’on utilise pour gérer la génération de dates et d'heures dans une application.

On utilise l’heure actuelle dans de nombreuses tâches comme par exemple la journalisation ou la planification d’événements. 
Traditionnellement, on utilise `DateTime.Now` ou `DateTime.UtcNow` pour obtenir la date et l’heure actuelles. 
Cependant, il y a un problème avec cette approche dans les situations où l’on souhaite tester des fonctionnalités dépendant de la date et de l'heure.

La date est extérieur au domain, c'est une dépendance. La valeur retournée n'est jamais la même. 
C'est pourquoi on doit isoler notre domaine via une abstraction et ainsi faciliter la testabilité,on peut utiliser un `DateTimeProvider`, une interface qui sert de fournisseur d'horloge abstraite.

Un `DateTimeProvider` est généralement une interface qui fournit une abstraction pour la date et l'heure.
On commence par définir une interface `IDateTimeProvider` :
```CSharp
public interface IDateTimeProvider
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
}
```

Cette interface expose deux propriétés, `Now` et `UtcNow`, pour obtenir respectivement l'heure locale et l'heure UTC.

On implémente cette interface avec une classe concrète `DateTimeProvider`:
```CSharp
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
}
```

Au lieu d'utiliser `DateTime.Now` directement dans une méthode, on injecte une instance de `IDateTimeProvider` :
```CSharp
public class ServiceAvecDate
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public ServiceAvecDate(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public void LogCurrentTime()
    {
        Console.WriteLine($"Heure actuelle : {_dateTimeProvider.Now}");
    }
}
```

# Avantages
1. **Testabilité** : On peut injecter une implémentation de `IDateTimeProvider` qui retourne une date fixe ou personnalisée, facilitant ainsi les tests des fonctionnalités dépendantes de l'heure.
2. **Centralisation** : Si l’on souhaite modifier la manière dont on obtient l'heure (par exemple pour gérer les fuseaux horaires ou ajouter une couche de cache), il suffit de modifier l’implémentation de `IDateTimeProvider`.
3. **Flexibilité** : L'abstraction permet d'introduire facilement des modifications ou des comportements alternatifs sans affecter le code métier.

# Exemple d'Implémentation pour les Tests
```CSharp
public class FixedDateTimeProvider : IDateTimeProvider
{
    private readonly DateTime _fixedDate;

    public FixedDateTimeProvider(DateTime fixedDate)
    {
        _fixedDate = fixedDate;
    }

    public DateTime Now => _fixedDate;
    public DateTime UtcNow => _fixedDate.ToUniversalTime();
}
```

Dans un test, on peut injecter `FixedDateTimeProvider` avec une date spécifique.

```CSharp
public void TestLogCurrentTime()
{
    var fixedDate = new DateTime(2023, 1, 1, 12, 0, 0);
    var dateTimeProvider = new FixedDateTimeProvider(fixedDate);

    var service = new ServiceAvecDate(dateTimeProvider);
    service.LogCurrentTime();  // Ici, il affichera toujours le 1er janvier 2023, 12:00
}
```

