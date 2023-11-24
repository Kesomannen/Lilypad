using System.Text;

namespace Lilypad; 

public readonly struct DataOperation {
    public readonly EnumReference<DataOperationType> OperationType;
    public readonly EnumReference<DataOperationSourceType> SourceType;
    
    public readonly DataSource? Source;
    public readonly NBTPath? SourcePath;
    public readonly object? NBTValue;

    public readonly int? Index;
    
    static readonly StringBuilder _builder = new();
    
    public DataOperation(
        EnumReference<DataOperationType> operation, 
        DataSource source, 
        NBTPath? sourcePath = null, 
        int? index = null
    ) {
        OperationType = operation;
        Source = source;
        SourcePath = sourcePath;
        Index = index;
        SourceType = DataOperationSourceType.From;
        
        CheckInsertIndex();
    }
    
    public DataOperation(
        EnumReference<DataOperationType> operation, 
        object value, 
        int? index = null
    ) {
        OperationType = operation;
        NBTValue = value;
        Index = index;
        SourceType = DataOperationSourceType.Value;
        
        CheckInsertIndex();
    }

    void CheckInsertIndex() {
        if (OperationType == DataOperationType.Insert && !Index.HasValue) {
            throw new InvalidOperationException("Insert operations require an index.");
        }
    }
    
    public override string ToString() {
        _builder.Clear();
       
        Append(OperationType);
        if (OperationType == DataOperationType.Insert) {
            Append(Index!);
        }
        
        Append(SourceType);
        if (SourceType == DataOperationSourceType.Value) {
            Append(NBTValue!);
        } else {
            Append(Source!);
            
            if (SourcePath is not null) {
                Append(SourcePath!);
            }
        }
        
        return _builder.ToString();
        
        void Append(object obj) {
            _builder.Append(obj);
            _builder.Append(' ');
        }
    }
    
    public static DataOperation Value(EnumReference<DataOperationType> operation, object value, int? index = null) {
        return new DataOperation(operation, value, index);
    }
    
    public static DataOperation From(EnumReference<DataOperationType> operation, DataSource source, NBTPath? sourcePath = null, int? index = null) {
        return new DataOperation(operation, source, sourcePath, index);
    }
}

public enum DataOperationType {
    Append,
    Insert,
    Merge,
    Prepend,
    Set
}

public enum DataOperationSourceType {
    From,
    Value
}