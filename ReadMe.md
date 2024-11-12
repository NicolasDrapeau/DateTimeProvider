# Pr�sentation
`DateTimeProvider` est un concept qui n'est pas un type int�gr� dans la BCL. 
Ce concept repr�sente une bonne pratique et un mod�le de conception qu�on utilise pour g�rer la g�n�ration de dates et d'heures dans une application.

On utilise l�heure actuelle dans de nombreuses t�ches comme par exemple la journalisation ou la planification d��v�nements. 
Traditionnellement, on utilise `DateTime.Now` ou `DateTime.UtcNow` pour obtenir la date et l�heure actuelles. 
Cependant, il y a un probl�me avec cette approche dans les situations o� l�on souhaite tester des fonctionnalit�s d�pendant de la date et de l'heure.

La date est ext�rieur au domain, c'est une d�pendance. La valeur retourn�e n'est jamais la m�me. 
C'est pourquoi on doit isoler notre domaine via une abstraction et ainsi faciliter la testabilit�,on peut utiliser un `DateTimeProvider`, une interface qui sert de fournisseur d'horloge abstraite.

Un `DateTimeProvider` est g�n�ralement une interface qui fournit une abstraction pour la date et l'heure.
On commence par d�finir une interface `IDateTimeProvider` :
```CSharp
public interface IDateTimeProvider
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
}
```

Cette interface expose deux propri�t�s, `Now` et `UtcNow`, pour obtenir respectivement l'heure locale et l'heure UTC.

On impl�mente cette interface avec une classe concr�te `DateTimeProvider`:
```CSharp
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
}
```

Au lieu d'utiliser `DateTime.Now` directement dans une m�thode, on injecte une instance de `IDateTimeProvider` :
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
1. **Testabilit�** : On peut injecter une impl�mentation de `IDateTimeProvider` qui retourne une date fixe ou personnalis�e, facilitant ainsi les tests des fonctionnalit�s d�pendantes de l'heure.
2. **Centralisation** : Si l�on souhaite modifier la mani�re dont on obtient l'heure (par exemple pour g�rer les fuseaux horaires ou ajouter une couche de cache), il suffit de modifier l�impl�mentation de `IDateTimeProvider`.
3. **Flexibilit�** : L'abstraction permet d'introduire facilement des modifications ou des comportements alternatifs sans affecter le code m�tier.

# Exemple d'Impl�mentation pour les Tests
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

Dans un test, on peut injecter `FixedDateTimeProvider` avec une date sp�cifique.

```CSharp
public void TestLogCurrentTime()
{
    var fixedDate = new DateTime(2023, 1, 1, 12, 0, 0);
    var dateTimeProvider = new FixedDateTimeProvider(fixedDate);

    var service = new ServiceAvecDate(dateTimeProvider);
    service.LogCurrentTime();  // Ici, il affichera toujours le 1er janvier 2023, 12:00
}
```

