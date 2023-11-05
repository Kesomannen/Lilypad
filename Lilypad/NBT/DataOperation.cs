using System.Text;

namespace Lilypad.NBT; 

public readonly struct DataOperation {
    readonly EnumReference<DataOperationType> _operationType;
    readonly EnumReference<DataOperationSourceType> _sourceType;
    
    readonly DataSource? _source;
    readonly NBTPath? _sourcePath;
    readonly NBTValue? _value;

    readonly int? _index;
    
    static readonly StringBuilder _builder = new();
    
    public DataOperation(
        EnumReference<DataOperationType> operation, 
        DataSource source, 
        NBTPath? sourcePath = null, 
        int? index = null
    ) {
        _operationType = operation;
        _source = source;
        _sourcePath = sourcePath;
        _index = index;
        _sourceType = DataOperationSourceType.From;
        
        CheckInsertIndex();
    }
    
    public DataOperation(
        EnumReference<DataOperationType> operation, 
        NBTValue value, 
        int? index = null
    ) {
        _operationType = operation;
        _value = value;
        _index = index;
        _sourceType = DataOperationSourceType.Value;
        
        CheckInsertIndex();
    }

    void CheckInsertIndex() {
        if (_operationType == DataOperationType.Insert && !_index.HasValue) {
            throw new InvalidOperationException("Insert operations require an index.");
        }
    }
    
    public override string ToString() {
        _builder.Clear();
       
        Append(_operationType);
        if (_operationType == DataOperationType.Insert) {
            Append(_index!);
        }
        
        Append(_sourceType);
        if (_sourceType == DataOperationSourceType.Value) {
            Append(_value!);
        } else {
            Append(_source!);
            
            if (_sourcePath.HasValue) {
                Append(_sourcePath!);
            }
        }
        
        return _builder.ToString();
        
        void Append(object obj) {
            _builder.Append(obj);
            _builder.Append(' ');
        }
    }
    
    public static DataOperation Value(EnumReference<DataOperationType> operation, NBTValue value, int? index = null) {
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